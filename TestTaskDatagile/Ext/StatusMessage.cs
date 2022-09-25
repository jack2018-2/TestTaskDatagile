namespace TestTaskDatagile.Ext
{
    /// <summary>
    /// Сообщение о статусе выполнения запроса
    /// </summary>
    public class StatusMessage
    {
        /// <summary>
        /// Текущий статус загрузки
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Кол-во загруженных изображений
        /// </summary>
        public int CountLoaded { get; set; }

        public StatusMessage(string message)
        {
            Status = message;
        }

        public StatusMessage(string message, int count)
        {
            Status = message;
            CountLoaded = count;
        }
    }
}
