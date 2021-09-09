using System.Collections.Generic;

namespace Test_IBS.Models
{
    /// <summary>
    /// Модель данных ResponseResult
    /// </summary>
    public class ResponseResult<T>
    {
        /// <summary>
        /// Признак успешности запроса
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Хранит ошибку, возникшую при выполнении запроса
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Список полученных данных
        /// </summary>
        public List<T> Data { get; set; }
    }
}
