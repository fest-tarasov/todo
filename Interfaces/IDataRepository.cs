using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS
{
    /// <summary>
    /// Интерфейс для работы с данными
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataRepository<T>
    {
        /// <summary>
        /// Получить список всех записей
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<T>> GetAsync();

        /// <summary>
        /// Получить список записей по указанному пользователю
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<ResponseResult<T>> GetAsync(int id);
    }
}
