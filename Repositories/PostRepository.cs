using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS
{
    /// <summary>
    /// Репозиторий для работы с постами пользователя
    /// </summary>
    public class PostRepository : IDataRepository<Post>
    {
        private readonly HttpClient _httpClient;

        public PostRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Получить список всех постов
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<Post>> GetAsync()
        {
            var result = new ResponseResult<Post>();

            try
            {
                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
                var jsonText = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<List<Post>>(jsonText);
                result.IsSuccess = true;
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Ошибка при получении списка постов: {err}";
            }

            return result;
        }

        /// <summary>
        /// Получить посты пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<ResponseResult<Post>> GetAsync(int userId)
        {
            var result = new ResponseResult<Post>();

            try
            {
                var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{userId}/posts");
                var jsonText = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<List<Post>>(jsonText);
                result.IsSuccess = true;
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Ошибка при получении списка постов пользователся с ID={userId}: {err}";
            }

            return result;
        }
    }
}
