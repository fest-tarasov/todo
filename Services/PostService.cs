using System.Linq;
using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS.Interfaces
{
    /// <summary>
    /// Сервис для получения данных по постах
    /// </summary>
    public class PostService : IPostService
    {
        private readonly IDataRepository<Post> _postRepository;

        public PostService(IDataRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        /// <summary>
        /// Получить список всех постов
        /// </summary>
        /// <returns></returns>
        async public Task<ResponseResult<Post>> GetAllAsync()
        {
            return await _postRepository.GetAsync();
        }

        /// <summary>
        /// Получить посты пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="take">Количество постов</param>
        /// <returns></returns>
        async public Task<ResponseResult<Post>> GetLastAsync(int userId, int take)
        {
            var result = await _postRepository.GetAsync(userId);
            if (result.IsSuccess)
            {
                result.Data = result.Data.OrderByDescending(x => x.id).Take(take).ToList();
            }
            return result;
        }
    }
}
