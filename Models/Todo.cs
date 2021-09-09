namespace Test_IBS.Models
{
    /// <summary>
    /// Модель данных Todo
    /// </summary>
    public class Todo
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
}
