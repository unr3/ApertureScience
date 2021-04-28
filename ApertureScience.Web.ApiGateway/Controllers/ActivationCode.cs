using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Messaging.Abstraction;
using ApertureScience.Web.ApiGateway.Event;
using ApertureScience.Web.ApiGateway.Services;
using ApertureScience.Web.ApiGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Controllers
{

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
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted,Type =typeof(ActivationCodeResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<ActivationCodeResponseViewModel>> GenerateCode(ActivationCodeRequestViewModel activationCodeRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest();

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
        [Produces("application/json")]
        public async Task<ActionResult<ActivationCodeResponseViewModel>> GenerateCodeBatch(ActivationCodeBatchRequestViewModel activationCodeBatchRequest )
        {
            if (!ModelState.IsValid)
                return BadRequest();

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
