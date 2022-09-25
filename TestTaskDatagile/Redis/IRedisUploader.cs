namespace TestTaskDatagile.Redis
{
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс загрузчика в Redis
    /// </summary>
    public interface IRedisUploader
    {
        /// <summary>
        /// Сохранить данные об изображениях в redis
        /// </summary>
        /// <param name="breed">Имя породы</param>
        /// <param name="response">Объект ответа API</param>
        public System.Threading.Tasks.Task Upload(string breed, Ext.DogeAPIResponse<IEnumerable<string>> response);
    }
}
