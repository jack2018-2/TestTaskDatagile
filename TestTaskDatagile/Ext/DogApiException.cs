using System;

namespace TestTaskDatagile.Ext
{
    /// <summary>
    /// Класс для ошибок, связанных с dog api 
    /// </summary>
    public class DogApiException : Exception
    {
        public DogApiException() { }
        public DogApiException(string message) : base(message) { }
    }
}
