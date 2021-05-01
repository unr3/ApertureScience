using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Extension;
using ApertureScience.Library.Messaging.Abstraction;
using ApertureScience.Web.ApiGateway.Event;
using ApertureScience.Web.ApiGateway.Event.ViewModels;
using ApertureScience.Web.ApiGateway.Services;
using ApertureScience.Web.ApiGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class Enrollment : ControllerBase
    {
        private readonly IQueueInfoService _queueInfoService;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IQueueManager _queueManager;
      
        public Enrollment(IQueueInfoService queueInfoService, IEventDispatcher eventDispatcher, IQueueManager queueManager)
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

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEnroll(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                return BadRequest("Message Id is empty");

            Guid requestedMessageId;
            if (!Guid.TryParse(messageId, out requestedMessageId))
                return BadRequest("Message Id is not valid");

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(EnrollmentRespondedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));
           
            _queueManager.CreateClient(queueInfo.QueueConnection, queueInfo.QueueName);

            bool messageFound = await _queueManager.PeekAsync(messageId);

            if (messageFound)
            {
                IMessage receivedMessage = await _queueManager.ReceiveAsync();
                var respondedEvent = receivedMessage.Body.ToString().ReadFromJson<EnrollmentRespondedEvent>();
                return Ok(respondedEvent.PayLoad.ToString());
            }
            else
                return NotFound();

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(EnrollmentResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<EnrollmentResponseViewModel>> Enroll([FromBody] EnrollmentViewModel enrollment)
        {


            enrollment.Password = HashService.Hash(enrollment.Password, $"{enrollment.Email}{enrollment.Code}", HashTypeEnum.Sha512);

            var requestedEvent = new EnrollmentRequestedEvent(Guid.NewGuid().ToString(), nameof(EnrollmentRequestedEvent), DateTime.UtcNow, enrollment);

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(EnrollmentRequestedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));

            _eventDispatcher.SetQueueConnectionAndName(queueInfo.QueueConnection, queueInfo.QueueName);
            string messageId = await _eventDispatcher.Dispatch(requestedEvent);
          
            var response = new EnrollmentResponseViewModel { Success = true, ResponseMessage = "Enrollment request accepted.", MessageId = messageId };
            return Accepted(response);
          


        }
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCheckIn(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                return BadRequest("Message Id is empty");

            Guid requestedMessageId;
            if (!Guid.TryParse(messageId, out requestedMessageId))
                return BadRequest("Message Id is not valid");

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(CheckInRespondedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));
            _queueManager.CreateClient(queueInfo.QueueConnection, queueInfo.QueueName);

            bool messageFound = await _queueManager.PeekAsync(messageId);

            if (messageFound)
            {
                IMessage receivedMessage = await _queueManager.ReceiveAsync();
                var respondedEvent = receivedMessage.Body.ToString().ReadFromJson<CheckInRespondedEvent>();
                return Ok(respondedEvent.PayLoad.ToString());
            }
            else
                return NotFound();

        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CheckInResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<CheckInResponseViewModel>> CheckIn(CheckInViewModel checkIn)
        {
           
         
            var requestedEvent = new CheckInRequestedEvent(Guid.NewGuid().ToString(), nameof(CheckInRequestedEvent), DateTime.UtcNow, checkIn);

            var queueInfo = _queueInfoService.GetQueueInfo(nameof(CheckInRequestedEvent));
            if (queueInfo == null)
                throw new ArgumentNullException(nameof(queueInfo));

            _eventDispatcher.SetQueueConnectionAndName(queueInfo.QueueConnection, queueInfo.QueueName);

            string messageId = await _eventDispatcher.Dispatch(requestedEvent);

            var response = new CheckInResponseViewModel { Success = true, ResponseMessage = "Check-In request accepted.", MessageId = messageId };
            return Accepted(response);
        }
    }
}
