namespace TestTaskDatagile.Services.Interface
{
    using System.Collections.Generic;
    using TestTaskDatagile.Ext;

    /// <summary>
    /// Интерфйес загрузчика списка пород
    /// </summary>
    public interface IDogBreedListLoader
    {
        /// <summary>
        /// Загрузить список пород собак
        /// </summary>
        public DogeAPIResponse<IDictionary<string, IEnumerable<string>>> Load();
    }
}
