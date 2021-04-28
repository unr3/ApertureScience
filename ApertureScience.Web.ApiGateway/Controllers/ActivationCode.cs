using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Messaging.Abstraction;
using ApertureScience.Web.ApiGateway.Event;
using ApertureScience.Web.ApiGateway.Services;
using ApertureScience.Web.ApiGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActivationCode(IQueueInfoService queueInfoService, IEventDispatcher  eventDispatcher, IQueueManager queueManager)
        {
            if (queueInfoService == null)
                throw new ArgumentNullException(nameof(queueInfoService));

            if (eventDispatcher == null)
                throw new ArgumentNullException(nameof(eventDispatcher));

            if (queueManager == null)
                throw new ArgumentNullException(nameof(queueManager));

            _queueInfoService = queueInfoService;
            _eventDispatcher = eventDispatcher;
            _queueManager = queueManager;
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

            var response = new ActivationCodeResponseViewModel { Success = true, ResponseMessage = "Request accepted.", MessageId = messageId };
            return Accepted(response);


        }
       
    }

   
}
