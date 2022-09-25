using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestTaskDatagile.Ext;
using TestTaskDatagile.Redis;
using TestTaskDatagile.Services.Interface;

namespace TestTaskDatagile.Services.Implementation
{
    public class ImageLoader : IImageLoader
    {
        private ILogger _logger;

        private IRedisUploader _redisUploader;

        /// <summary>
        /// URL API списка фотографий породы
        /// </summary>
        private string _URL;

        public ImageLoader(IConfiguration configuration, ILogger<ImageLoader> logger, IRedisUploader redisUploader)
        {
            _logger = logger;
            _redisUploader = redisUploader;
            _URL = configuration.GetSection("BreedImageListURL").Value;
        }

        public async Task Load(KeyValuePair<string, IEnumerable<string>> breed, int count)
        {
            var response = CallApi(breed, count);
            await _redisUploader.Upload(breed.Key, response);
        }

        private DogeAPIResponse<IEnumerable<string>> CallApi(KeyValuePair<string, IEnumerable<string>> breed, int count)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseRaw = client
                        .GetAsync(GetUrl(breed, count))
                        .Result;

                    var parsedResponse = JsonSerializer
                        .Deserialize<DogeAPIResponse<IEnumerable<string>>>(responseRaw.Content.ReadAsStringAsync().Result);

                    //todo обработка status != succes?

                    return parsedResponse;
                }
            }
            catch (Exception e) when (e is HttpRequestException ||
                                      e is InvalidOperationException ||
                                      e is TaskCanceledException ||
                                      e is ArgumentNullException ||
                                      e is JsonException ||
                                      e is NotSupportedException)
            {
                var msg = $"Не удалось выгрузить фотографии породы {breed.Key}";
                throw new DogApiException(msg);
            }
        }

        private string GetUrl(KeyValuePair<string, IEnumerable<string>> breed, int count)
            => _URL.Replace("{breed}", breed.Key.ToLower()).Replace("{count}", count.ToString());
    }
}
