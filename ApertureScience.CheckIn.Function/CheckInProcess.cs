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

namespace ApertureScience.CheckIn.Function
{
    public  class CheckInProcess
    {
        private readonly ICheckInProcessor _processor;
        public CheckInProcess(ICheckInProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("CheckInProcess")]
        [return: Queue("%ResponseQueue%", Connection = "AzureWebJobsStorage")]
        public async Task<BasicMessage> Run([QueueTrigger("%TriggerQueue%", Connection = "AzureWebJobsStorage")]string requestedEventMessage, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {requestedEventMessage}");

            BasicMessage message = JsonConvert.DeserializeObject<BasicMessage>(requestedEventMessage);

            var requestedEvent = message.Body.ToString().ReadFromJson<CheckInRequestedEvent>();
            var checkIn = requestedEvent.PayLoad.ToString().ReadFromJson<CheckInViewModel>();
            var result = await _processor.CheckIn(checkIn.Email);


            var respondedEvent = new CheckInRespondedEvent(Guid.NewGuid().ToString(), nameof(CheckInRespondedEvent), DateTime.UtcNow, result);
            BasicMessage returnMessage = new BasicMessage(message.MessageId, respondedEvent.GetType().AssemblyQualifiedName, respondedEvent);

            return returnMessage;

        }
    }
}
