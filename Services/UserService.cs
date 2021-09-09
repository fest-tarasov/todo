using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS.Interfaces
{
    /// <summary>
    /// Сервис для получения данных по пользователям
    /// </summary>
    public class UserService: IUserService
    {
        private readonly IDataRepository<User> _userRepository;

        public UserService(IDataRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        async public Task<ResponseResult<User>> GetAllAsync()
        {
            var result = await _userRepository.GetAsync();
            return result;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        async public Task<ResponseResult<User>> GetAsync(int userId)
        {
            var result = await _userRepository.GetAsync(userId);
            return result;
        }
    }
}
