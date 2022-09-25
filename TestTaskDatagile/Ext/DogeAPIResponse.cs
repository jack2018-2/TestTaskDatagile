using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestTaskDatagile.Ext
{
    /// <summary>
    /// Общий класс для ответов API собак
    /// </summary>
    /// <typeparam name="T">Тип контента, лежащего в поле message ответа</typeparam>
    public class DogeAPIResponse<T>
    {
        [JsonPropertyName("message")]
        public T Result { get; set; }

        /// <summary>
        /// Статус запроса
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
