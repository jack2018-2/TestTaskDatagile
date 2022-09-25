using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskDatagile.Ext;

namespace TestTaskDatagile.Services.Interface
{
    /// <summary>
    /// Интерфейс загрузчика изображений
    /// </summary>
    public interface IImageLoader
    {
        /// <summary>
        /// Загрузить изображения через API и сохранить в redis
        /// </summary>
        public Task Load (KeyValuePair<string, IEnumerable<string>> breed, int count);
    }
}
