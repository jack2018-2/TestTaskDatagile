﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TestTaskDatagile.Ext;
using TestTaskDatagile.Services.Interface;

namespace TestTaskDatagile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogApiController : ControllerBase
    {

        private readonly ILogger<DogApiController> _logger;
        private readonly IDogeRequestProcessor _requestProcessor;

        public DogApiController(ILogger<DogApiController> logger, IDogeRequestProcessor requestProcessor)
        {
            _logger = logger;
            _requestProcessor = requestProcessor;
        }

        [HttpPost]
        public async Task<StatusMessage> Post([FromBody] DogeRequest request)
        {
            try
            {
                return await _requestProcessor.Process(request);
            }
            catch(DogApiException e)
            {
                return new StatusMessage(e.Message);
            }
            catch(Exception e)
            {
                return new StatusMessage("Произошла непредвиденная ошибка");
            }
        }
    }
}
