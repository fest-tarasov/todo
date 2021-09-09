using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с задачами пользователя
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Получить список всех задач
        /// </summary>
        /// <returns></returns>
        public Task<ResponseResult<Todo>> GetAllAsync();

        /// <summary>
        /// Получить задачи пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public Task<ResponseResult<Todo>> GetCompletedAsync(int userId);
    }
}
