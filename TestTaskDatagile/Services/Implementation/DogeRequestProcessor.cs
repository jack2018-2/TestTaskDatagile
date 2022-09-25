using Microsoft.Extensions.Logging;
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

        public DogeRequestProcessor(IDogBreedListLoader breedLoader, IImageLoader imageLoader, ILogger<DogeRequestProcessor> logger)
        {
            _breedLoader = breedLoader;
            _imageLoader = imageLoader;
            _logger = logger;
        }

        public async Task<StatusMessage> Process(DogeRequest request)
        {
            if (_isRunning)
            {
                _logger.LogWarning("Попытка запуска обработки при уже запущенном процессе.");
                return new StatusMessage("run");
            }

            _isRunning = true;
            _logger.LogInformation($"Запущен процесс обработки с параметрами:|запрос: {request.Command}|число фотографий: {request.Count}");

            var breeds = _breedLoader.Load();

            foreach (var breed in breeds.Result)
            {
                try
                {
                    await _imageLoader.Load(breed, request.Count);
                }
                catch (DogApiException e)
                {
                    _isRunning = false;
                    _logger.LogError(e, e.Message);
                    throw e;
                }
            }
            _isRunning = false;
            return new StatusMessage("ok");
        }
    }
}
