using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Extension;
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
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserData(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                return BadRequest("Message Id is empty");

            Guid requestedMessageId;
            if (!Guid.TryParse(messageId, out requestedMessageId))
                return BadRequest("Message Id is not valid");

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(AccelerometerIngestUserDataRespondedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));

            _queueManager.CreateClient(queueInfo.QueueConnection, queueInfo.QueueName);

            bool messageFound = await _queueManager.PeekAsync(messageId);

            if (messageFound)
            {
                IMessage receivedMessage = await _queueManager.ReceiveAsync();
                var respondedEvent = receivedMessage.Body.ToString().ReadFromJson<AccelerometerIngestUserDataRespondedEvent>();
                return Ok(respondedEvent.PayLoad.ToString());
            }
            else
                return NotFound();

        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(IngestDataResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<IngestDataResponseViewModel>> UserData([FromBody] UserDataViewModel userData)
        {
            int pageSize;
            if (!int.TryParse(_configuration["PageSize"], out pageSize) || pageSize <= 0)
                throw new ArgumentNullException(nameof(pageSize));


            var queueInfo = _queueInfoService.GetQueueInfo(nameof(AccelerometerIngestUserDataRequestedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));


            var requestedEvent = new AccelerometerIngestUserDataRequestedEvent(Guid.NewGuid().ToString(), nameof(AccelerometerIngestUserDataRequestedEvent), DateTime.UtcNow, userData.UserId, userData.TimeStamp, userData.Page, pageSize);

            _eventDispatcher.SetQueueConnectionAndName(queueInfo.QueueConnection, queueInfo.QueueName);
            string messageId = await _eventDispatcher.Dispatch(requestedEvent);

            var response = new IngestDataResponseViewModel { Success = true, ResponseMessage = "UserData request accepted.", MessageId = messageId };

            return Accepted(response);


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
