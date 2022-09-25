using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskDatagile.Redis
{
    /// <summary>
    /// Интерфейс загрузчика в Redis
    /// </summary>
    public interface IRedisUploader
    {
        public void Upload(string breed, Ext.DogeAPIResponse<IEnumerable<string>> response);
    }
}
