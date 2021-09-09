using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS
{
    /// <summary>
    /// Репозиторий для работы с задачами пользователя
    /// </summary>
    public class TodoRepository : IDataRepository<Todo>
    {
        private readonly HttpClient _httpClient;

        public TodoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Получить список всех задач
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<Todo>> GetAsync()
        {
            var result = new ResponseResult<Todo>();

            try
            {
                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
                var jsonText = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<List<Todo>>(jsonText);
                result.IsSuccess = true;
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Ошибка при получении списка задач: {err}";
            }

            return result;
        }

        /// <summary>
        /// Получить задачи пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<ResponseResult<Todo>> GetAsync(int userId)
        {
            var result = new ResponseResult<Todo>();

            try
            {
                var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{userId}/todos?completed=true");
                var jsonText = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<List<Todo>>(jsonText);
                result.IsSuccess = true;
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Ошибка при получении списка задач пользователся с ID={userId}: {err}";
            }

            return result;
        }
    }
}
