using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestTaskDatagile.Ext
{
    /// <summary>
    /// Обертка для ответа api со списком пород
    /// </summary>
    public class BreedsListResponse
    {
        /// <summary>
        /// Список пород
        /// </summary>
        [JsonPropertyName("message")]
        public IDictionary<string, IEnumerable<string>> Result { get; set; }

        /// <summary>
        /// Статус запроса
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
