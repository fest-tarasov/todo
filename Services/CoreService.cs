using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test_IBS.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Test_IBS
{
    /// <summary>
    /// Основной сервис для работы программы
    /// </summary>
    public class CoreService : ICoreService
    {
        private readonly IUserService _userService;
        private readonly ITodoService _todoService;
        private readonly IPostService _postService;
        private readonly IConfiguration _config;

        public CoreService(IUserService userService, ITodoService todoService, IPostService postService, IConfiguration config)
        {
            _userService = userService;
            _todoService = todoService;
            _postService = postService;
            _config = config;
        }

        /// <summary>
        /// Запуск ожидания ввода информации пользователя
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            while (true)
            {
                Console.WriteLine("Введите Id пользователя или латинскую букву 'a' для получения списка всех пользователей:");

                string input = Console.ReadLine();

                //Выводим список всех пользователей
                if (string.Equals(input, "a", StringComparison.OrdinalIgnoreCase))
                {
                    await PrintAllUsers();
                }
                //Выводим информацию по указанному пользователю
                else if (int.TryParse(input, out int id) != default)
                {
                    await PrintUserData(id);
                }
                //Неверное значение
                else
                {
                    Console.WriteLine("Вы ввели неверное значение");
                }

                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Вывести в консоль список всех пользователей
        /// </summary>
        /// <returns></returns>
        private async Task PrintAllUsers()
        {
            var usersResponse = await _userService.GetAllAsync();
            if (usersResponse.IsSuccess)
            {
                foreach (var user in usersResponse.Data)
                {
                    Console.WriteLine($"ID={user.id} Имя: {user.name}");
                }
            }
            else
            {
                Console.WriteLine(usersResponse.ErrorMessage);
            }
        }

        /// <summary>
        /// Вывести в консоль данные пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        private async Task PrintUserData(int userId)
        {
            var usersResponse = await _userService.GetAsync(userId);
            if (usersResponse.IsSuccess)
            {
                //Пользователь с указанным Id найден, можем запросить остальные данные
                if (usersResponse.Data.Any())
                {
                    var user = usersResponse.Data.FirstOrDefault();
                    var todosTask = _todoService.GetCompletedAsync(user.id);
                    var postsTask = _postService.GetLastAsync(user.id, take: 5);
                    await Task.WhenAll(todosTask, postsTask);

                    //User
                    var sb = new StringBuilder("********************************************************");
                    sb.AppendLine().AppendLine($"Уважаемый {user.name}.");
                    sb.AppendLine("Ниже представлен список ваших действий за последнее время.");

                    //Todos
                    if (todosTask.Result.IsSuccess)
                    {
                        if (todosTask.Result.Data.Any())
                        {
                            sb.AppendLine().AppendLine("Выполнено задач:");
                            foreach (var post in todosTask.Result.Data)
                            {
                                sb.AppendLine(post.title);
                            }
                        }
                        else
                        {
                            sb.AppendLine("Нет данных");
                        }
                    }
                    else
                    {
                        sb.AppendLine(todosTask.Result.ErrorMessage);
                    }

                    //Posts
                    if (postsTask.Result.IsSuccess)
                    {
                        if (postsTask.Result.Data.Any())
                        {
                            sb.AppendLine().AppendLine("Написано постов:");
                            foreach (var post in postsTask.Result.Data)
                            {
                                sb.AppendLine(post.title);
                            }
                        }
                        else
                        {
                            sb.AppendLine("Нет данных");
                        }
                    }
                    else
                    {
                        sb.AppendLine(postsTask.Result.ErrorMessage);
                    }

                    Console.WriteLine(sb);
                    SaveLog(sb);
                }
                else
                {
                    Console.WriteLine($"Пользователь с ID '{userId}' не найден в базе");
                }
            }
            else
            {
                Console.WriteLine(usersResponse.ErrorMessage);
            }
        }

        /// <summary>
        /// Сохранить данные в файл
        /// </summary>
        /// <param name="sb">Данные для сохранения</param>
        private void SaveLog(StringBuilder sb)
        {
            try
            {
                var pathToLogFolder = _config["PathTo:LogFolder"];
                if (!Directory.Exists(pathToLogFolder))
                {
                    Directory.CreateDirectory(pathToLogFolder);
                }
                var pathToFile = Path.Combine(pathToLogFolder, "log.txt");

                using (StreamWriter sw = File.AppendText(pathToFile))
                {
                    sw.WriteLine(sb);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine($"Не удалось сохранить данные в файл: {err}");
            }            
        }
    }
}
