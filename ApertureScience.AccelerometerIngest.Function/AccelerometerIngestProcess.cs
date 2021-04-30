using System;
using System.Threading.Tasks;
using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Extension;
using ApertureScience.Library.Messaging.Implementation;
using ApertureScience.Web.ApiGateway.Event;
using ApertureScience.Web.ApiGateway.Event.ViewModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApertureScience.AccelerometerIngest.Function
{
    public  class AccelerometerIngestProcess
    {
        private readonly IAccelerometerIngestProcessor _processor;
        public AccelerometerIngestProcess(IAccelerometerIngestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("AccelerometerIngestProcess")]
        [return: Queue("%ResponseQueue%", Connection = "AzureWebJobsStorage")]
        public async Task<BasicMessage> Run([QueueTrigger("%TriggerQueue%", Connection = "AzureWebJobsStorage")]string requestedEventMessage, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {requestedEventMessage}");

            BasicMessage message = JsonConvert.DeserializeObject<BasicMessage>(requestedEventMessage);


            var requestedEvent = message.Body.ToString().ReadFromJson<AccelerometerIngestRequestedEvent>();

            var viewModelArray = requestedEvent.PayLoad.ToString().ReadFromJson<IngestDataViewModel[]>();
            var dataArray = IngestViewModelToIngestEntitiy.From(viewModelArray);
            var result = await _processor.IngestData(dataArray);


            IEvent respondedEvent = new AccelerometerIngestRespondedEvent(Guid.NewGuid().ToString(), nameof(ActivationCodeBatchRespondedEvent), DateTime.UtcNow,result);
            BasicMessage returnMessage = new BasicMessage(message.MessageId, respondedEvent.GetType().AssemblyQualifiedName, respondedEvent);

            return returnMessage;

           
        }
    }
}
