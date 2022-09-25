namespace TestTaskDatagile.Ext
{
    /// <summary>
    /// Запрос на запуск загрузки изображений
    /// </summary>
    public class DogeRequest
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Кол-во изображений для загрузки в каждой породе
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Имя породы, для которой нужно загрузить фотографии
        /// </summary>
        public string BreedName { get; set; }
    }
}
