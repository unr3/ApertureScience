using ApertureScience.Library.Event.Abstraction;
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
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(EnrollmentResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<EnrollmentResponseViewModel>> Enroll([FromBody] EnrollmentViewModel enrollment)
        {

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
    }
}
