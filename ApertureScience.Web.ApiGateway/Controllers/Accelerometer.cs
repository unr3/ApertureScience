using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Messaging.Abstraction;
using ApertureScience.Web.ApiGateway.Event;
using ApertureScience.Web.ApiGateway.Event.ViewModels;
using ApertureScience.Web.ApiGateway.Services;
using ApertureScience.Web.ApiGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class Accelerometer : ControllerBase
    {
        private readonly IQueueInfoService _queueInfoService;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IQueueManager _queueManager;
        private readonly IConfiguration _configuration;

        public Accelerometer(IQueueInfoService queueInfoService, IEventDispatcher eventDispatcher, IQueueManager queueManager, IConfiguration configuration)
        {
            if (queueInfoService == null)
                throw new ArgumentNullException(nameof(queueInfoService));

            if (eventDispatcher == null)
                throw new ArgumentNullException(nameof(eventDispatcher));

            if (queueManager == null)
                throw new ArgumentNullException(nameof(queueManager));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _queueInfoService = queueInfoService;
            _eventDispatcher = eventDispatcher;
            _queueManager = queueManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(IngestDataResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<IngestDataResponseViewModel>>IngestData(IngestDataViewModel[] data)
        {
            if (data == null || data.Length == 0)
                return BadRequest("Data is empty");


            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return BadRequest(errors);
            }

            int ingestBlockSize;

            if (!int.TryParse(_configuration["IngestBlockSize"], out ingestBlockSize) || ingestBlockSize <= 0)
                throw new ArgumentNullException(nameof(ingestBlockSize));

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(AccelerometerIngestRequestedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));

            _eventDispatcher.SetQueueConnectionAndName(queueInfo.QueueConnection, queueInfo.QueueName);

            int dataParts = (data.Length + ingestBlockSize - 1) / ingestBlockSize;

            List<string> messageIdList = new List<string>();
            for (int i = 0; i < dataParts; i++)
            {
                var requestedEvent = new AccelerometerIngestRequestedEvent(Guid.NewGuid().ToString(), nameof(AccelerometerIngestRequestedEvent), DateTime.UtcNow, data.Skip(i * ingestBlockSize).Take(ingestBlockSize));

                string messageId = await _eventDispatcher.Dispatch(requestedEvent);
                messageIdList.Add(messageId);
            }

            string responseMessageId = null;
            if (messageIdList.Count == 1)
                responseMessageId = messageIdList[0];
            else
                responseMessageId = String.Join(",", messageIdList);

            var response = new IngestDataResponseViewModel { Success = true, ResponseMessage = "IngestData request accepted.", MessageId = responseMessageId };

            return Accepted(response);

        }
    }
}
