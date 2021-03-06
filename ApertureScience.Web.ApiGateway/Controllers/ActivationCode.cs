using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Extension;
using ApertureScience.Library.Messaging.Abstraction;
using ApertureScience.Web.ApiGateway.Commons;
using ApertureScience.Web.ApiGateway.Event;
using ApertureScience.Web.ApiGateway.Services;
using ApertureScience.Web.ApiGateway.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Controllers
{

    [Authorize(Roles =Role.Admin)]
    [Route("[controller]/[action]")]
    [ApiController]
    public class ActivationCode : ControllerBase
    {
        private readonly IQueueInfoService _queueInfoService;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IQueueManager _queueManager;
        private readonly IConfiguration _configuration;
        public ActivationCode(IQueueInfoService queueInfoService, IEventDispatcher  eventDispatcher, IQueueManager queueManager,IConfiguration configuration)
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
        /// <summary>
        /// Get result from requested generatecode action 
        /// </summary>
        /// <param name="messageId">Id for requested event message</param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetActivationCode(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                return BadRequest("Message Id is empty");

            Guid requestedMessageId;
            if (!Guid.TryParse(messageId, out requestedMessageId))
                return BadRequest("Message Id is not valid");

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(ActivationCodeRespondedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));

            _queueManager.CreateClient(queueInfo.QueueConnection, queueInfo.QueueName);

            bool messageFound = await _queueManager.PeekAsync(messageId);

            if (messageFound)
            {
                IMessage receivedMessage = await _queueManager.ReceiveAsync();
                var respondedEvent = receivedMessage.Body.ToString().ReadFromJson<ActivationCodeRespondedEvent>();
                return Ok(respondedEvent.Result.ToString());
            }
            else
                return NotFound();

        }
        /// <summary>
        /// Get result from requested generatecode batch action 
        /// </summary>
        /// <param name="messageId">Id for requested event message</param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetActivationBatchCode(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                return BadRequest("Message Id is empty");

            Guid requestedMessageId;
            if (!Guid.TryParse(messageId, out requestedMessageId))
                return BadRequest("Message Id is not valid");

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(ActivationCodeBatchRespondedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));

            _queueManager.CreateClient(queueInfo.QueueConnection, queueInfo.QueueName);

            bool messageFound = await _queueManager.PeekAsync(messageId);

            if (messageFound)
            {
                IMessage receivedMessage = await _queueManager.ReceiveAsync();
                var respondedEvent = receivedMessage.Body.ToString().ReadFromJson<ActivationCodeBatchRespondedEvent>();
                return Ok(respondedEvent.Result.ToString());
            }
            else
                return NotFound();

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted,Type =typeof(ActivationCodeResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<ActionResult<ActivationCodeResponseViewModel>> GenerateCode(ActivationCodeRequestViewModel activationCodeRequest)
        {


            var requestedEvent = new ActivationCodeRequestedEvent(Guid.NewGuid().ToString(), nameof(ActivationCodeRequestedEvent), DateTime.UtcNow, 1, activationCodeRequest.IsAdmin);

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(ActivationCodeRequestedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));

            _eventDispatcher.SetQueueConnectionAndName(queueInfo.QueueConnection, queueInfo.QueueName);
            string messageId = await _eventDispatcher.Dispatch(requestedEvent);

            var response = new ActivationCodeResponseViewModel { Success = true, ResponseMessage = "ActivationCode request accepted.", MessageId = messageId };
            return Accepted(response);


        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ActivationCodeResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<ActionResult<ActivationCodeResponseViewModel>> GenerateCodeBatch(ActivationCodeBatchRequestViewModel activationCodeBatchRequest )
        {
            

            if (activationCodeBatchRequest.BatchSize<= 0)
            {
                return BadRequest();
            }

            int maxBatchSize = int.Parse(_configuration["ActivationCodeMaxBatchsize"]);

            if (activationCodeBatchRequest.BatchSize  > maxBatchSize)
            {
                activationCodeBatchRequest.BatchSize = maxBatchSize;
            }

            var requestedEvent = new ActivationCodeBatchRequestedEvent(Guid.NewGuid().ToString(), nameof(ActivationCodeRequestedEvent), DateTime.UtcNow, activationCodeBatchRequest.BatchSize);
           
            var queueInfo = _queueInfoService.GetQueueInfo(nameof(ActivationCodeBatchRequestedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));

            _eventDispatcher.SetQueueConnectionAndName(queueInfo.QueueConnection, queueInfo.QueueName);
            string messageId = await _eventDispatcher.Dispatch(requestedEvent);
            
            var response = new ActivationCodeResponseViewModel { Success = true, ResponseMessage = "ActivationCode batch request accepted.", MessageId = messageId };
            return Accepted(response);
        }

    }

   
}
