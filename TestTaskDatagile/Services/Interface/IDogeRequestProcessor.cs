using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskDatagile.Ext;

namespace TestTaskDatagile.Services.Interface
{
    /// <summary>
    /// Интерфейс процессора запросов
    /// </summary>
    public interface IDogeRequestProcessor
    {
        /// <summary>
        /// Обработать запрос
        /// </summary>
        /// <param name="request">Объект запроса</param>
        /// <returns>объект <see cref="StatusMessage"/> с информацией об успешности обработки</returns>
        public Task<StatusMessage> Process(DogeRequest request);
    }
}
