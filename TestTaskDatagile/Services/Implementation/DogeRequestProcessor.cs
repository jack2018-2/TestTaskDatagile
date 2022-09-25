using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestTaskDatagile.Ext;
using TestTaskDatagile.Services.Interface;

namespace TestTaskDatagile.Services.Implementation
{
    public class DogeRequestProcessor : IDogeRequestProcessor
    {
        private IDogBreedListLoader _breedLoader;
        private IImageLoader _imageLoader;
        private ILogger _logger;

        private bool _isRunning;
        private int _currentBatchSize;
        private int _countLoaded;

        public DogeRequestProcessor(IDogBreedListLoader breedLoader, IImageLoader imageLoader, ILogger<DogeRequestProcessor> logger)
        {
            _breedLoader = breedLoader;
            _imageLoader = imageLoader;
            _logger = logger;
            _countLoaded = 0;
        }

        public async Task<StatusMessage> Process(DogeRequest request)
        {
            if (_isRunning)
            {
                _logger.LogWarning("Попытка запуска обработки при уже запущенном процессе");
                return new StatusMessage("run", _countLoaded * _currentBatchSize);
            }

            _currentBatchSize = request.Count;
            _isRunning = true;
            _logger.LogInformation($"Запущен процесс обработки с параметрами|запрос: {request.Command}|число фотографий: {request.Count}");
            var breeds = _breedLoader.Load();
            
            if (request.BreedName is null)
            {
                await ProcessImages(breeds.Result, request.Count);
            }
            else
            {
                if (breeds.Result.ContainsKey(request.BreedName))
                {
                    var t = new Dictionary<string, IEnumerable<string>>();
                    t.Add(request.BreedName, new List<string>());
                    await ProcessImages(t, request.Count);
                }
                else
                {
                    _logger.LogWarning("Попытка запуска с некорректным именем породы");
                    return new StatusMessage("denied", 0);
                }
            }

            var countProcessed = _countLoaded * _currentBatchSize;
            _logger.LogInformation($"Завершено сохранение изображений, сохранено {countProcessed} объектов");
            FinishProcess();
            return new StatusMessage("ok", countProcessed);
        }

        private async Task ProcessImages(IDictionary<string, IEnumerable<string>> breeds, int count)
        {
            _logger.LogInformation("Начато получение и сохранение изображений в Redis");
            foreach (var breed in breeds)
            {
                try
                {
                    await _imageLoader.Load(breed, count);
                    Interlocked.Increment(ref _countLoaded);
                }
                catch (DogApiException e)
                {
                    FinishProcess();
                    _logger.LogError(e, e.Message);
                    throw e;
                }
            }
        }

        private void FinishProcess()
        {
            _countLoaded = 0;
            _isRunning = false;
        }
    }
}
