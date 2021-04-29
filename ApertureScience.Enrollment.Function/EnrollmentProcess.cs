using System;
using System.Threading.Tasks;
using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Extension;
using ApertureScience.Library.Messaging.Implementation;
using ApertureScience.Web.ApiGateway.Event;
using ApertureScience.Web.ApiGateway.Event.ViewModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApertureScience.Enrollment.Function
{
    public  class EnrollmentProcess
    {
        private readonly IEnrollmentProcessor _processor;

        public EnrollmentProcess(IEnrollmentProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("EnrollmentProcess")]
        [return: Queue("%ResponseQueue%", Connection = "AzureWebJobsStorage")]
        public async Task<BasicMessage> Run([QueueTrigger("%TriggerQueue%", Connection = "AzureWebJobsStorage")]string requestedEventMessage, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {requestedEventMessage}");

            BasicMessage message = JsonConvert.DeserializeObject<BasicMessage>(requestedEventMessage);
            var requestedEvent = message.Body.ToString().ReadFromJson<EnrollmentRequestedEvent>();

            EnrollmentViewModel enrollment =requestedEvent.PayLoad.ToString().ReadFromJson<EnrollmentViewModel>();

            var result = await _processor.Enroll(enrollment);

            IEvent respondedEvent = new EnrollmentRespondedEvent(Guid.NewGuid().ToString(), nameof(EnrollmentRespondedEvent), DateTime.UtcNow, result);
            BasicMessage returnMessage = new BasicMessage(message.MessageId, respondedEvent.GetType().AssemblyQualifiedName, respondedEvent);
           

            return returnMessage;

            
        }
    }
}
