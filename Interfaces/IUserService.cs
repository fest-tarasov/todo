using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS.Interfaces
{
    /// <summary>
    /// Интерфейс для получения данных по пользователям
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        public Task<ResponseResult<User>> GetAllAsync();

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public Task<ResponseResult<User>> GetAsync(int userId);
    }
}
