using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с постами пользователя
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Получить список всех постов
        /// </summary>
        /// <returns></returns>
        public Task<ResponseResult<Post>> GetAllAsync();

        /// <summary>
        /// Получить посты пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="take">Количество постов</param>
        /// <returns></returns>
        public Task<ResponseResult<Post>> GetLastAsync(int userId, int take);
    }
}
