using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS.Interfaces
{
    /// <summary>
    /// Сервис для получения данных по задачам
    /// </summary>
    public class TodoService : ITodoService
    {
        private readonly IDataRepository<Todo> _todoRepository;

        public TodoService(IDataRepository<Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        /// <summary>
        /// Получить список всех задач
        /// </summary>
        /// <returns></returns>
        async public Task<ResponseResult<Todo>> GetAllAsync()
        {
            return await _todoRepository.GetAsync();
        }

        /// <summary>
        /// Получить задачи пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        async public Task<ResponseResult<Todo>> GetCompletedAsync(int userId)
        {
            return await _todoRepository.GetAsync(userId);
        }
    }
}
