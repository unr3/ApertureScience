using System;
using System.Threading.Tasks;
using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Extension;
using ApertureScience.Library.Messaging.Implementation;
using ApertureScience.Web.ApiGateway.Event;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApertureScience.AccelerometerIngestUserData.Function
{
    public  class AccelerometerIngestUserDataProcess
    {
        private readonly IUserDataIngestProcessor _processor;
        public AccelerometerIngestUserDataProcess(IUserDataIngestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("AccelerometerIngestUserDataProcess")]
        [return: Queue("%ResponseQueue%", Connection = "AzureWebJobsStorage")]
        public async Task<BasicMessage> Run([QueueTrigger("%TriggerQueue%", Connection = "AzureWebJobsStorage")]string requestedEventMessage, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {requestedEventMessage}");

            BasicMessage message = JsonConvert.DeserializeObject<BasicMessage>(requestedEventMessage);


            var requestedEvent = message.Body.ToString().ReadFromJson<AccelerometerIngestUserDataRequestedEvent>();

            var result = await _processor.GetUserIngestData(requestedEvent.UserId,requestedEvent.TimeStamp,requestedEvent.Page,requestedEvent.PageSize);


            IEvent respondedEvent = new AccelerometerIngestUserDataRespondedEvent(Guid.NewGuid().ToString(), nameof(AccelerometerIngestUserDataRespondedEvent), DateTime.UtcNow, result);
            BasicMessage returnMessage = new BasicMessage(message.MessageId, respondedEvent.GetType().AssemblyQualifiedName, respondedEvent);

            return returnMessage;
            
        }
    }
}
