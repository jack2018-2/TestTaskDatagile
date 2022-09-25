using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using TestTaskDatagile.Ext;
using TestTaskDatagile.Services.Interface;

namespace TestTaskDatagile.Services.Implementation
{
    public class DogBreedListLoader : IDogBreedListLoader
    {
        private DogeAPIResponse<IDictionary<string, IEnumerable<string>>> _apiResponse;

        private ILogger _logger;

        private string _URL;

        public DogBreedListLoader(IConfiguration configuration, ILogger<DogBreedListLoader> logger)
        {
            _logger = logger;
            _URL = configuration.GetSection("BreedListURL").Value;
        }

        public DogeAPIResponse<IDictionary<string, IEnumerable<string>>> Load()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseRaw = client.GetAsync(_URL).Result;
                    var json = responseRaw.Content.ReadAsStringAsync().Result;
                    _apiResponse = JsonSerializer.Deserialize<DogeAPIResponse<IDictionary<string, IEnumerable<string>>>>(json);
                }
                //todo обработка status != succes?
                return _apiResponse;
            }
            catch (Exception e) when (e is HttpRequestException ||
                                      e is InvalidOperationException ||
                                      e is System.Threading.Tasks.TaskCanceledException ||
                                      e is ArgumentNullException ||
                                      e is JsonException ||
                                      e is NotSupportedException)
            {
                var msg = "Не удалось выгрузить список пород";
                _logger.LogError(msg, e);
                throw new DogApiException(msg);
            }
        }
    }
}
