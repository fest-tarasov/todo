using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Test_IBS.Models;

namespace Test_IBS
{
    /// <summary>
    /// Репозиторий для получения данных по пользователям
    /// </summary>
    public class UserRepository: IDataRepository<User>
    {
        private readonly HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<User>> GetAsync()
        {
            var result = new ResponseResult<User>();

            try
            {
                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
                var jsonText = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<List<User>>(jsonText);
                result.IsSuccess = true;
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Ошибка при получении списка пользователей: {err}";
            }

            return result;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<ResponseResult<User>> GetAsync(int id)
        {
            var result = new ResponseResult<User>();

            try
            {
                var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users?id={id}");
                var jsonText = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<List<User>>(jsonText);
                result.IsSuccess = true;
            }
            catch (Exception err)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Ошибка при получении пользователя по ID={id}: {err}";
            }

            return result;
        }
    }
}
