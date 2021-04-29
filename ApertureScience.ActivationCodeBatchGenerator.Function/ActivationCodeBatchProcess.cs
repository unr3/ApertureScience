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

namespace ApertureScience.ActivationCodeBatchGenerator.Function
{
    public  class ActivationCodeBatchProcess
    {
        private readonly IActivationCodeBatchProcessor _processor;
        public ActivationCodeBatchProcess(IActivationCodeBatchProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ActivationCodeBatchProcess")]
        [return: Queue("%ResponseQueue%", Connection = "AzureWebJobsStorage")]
        public async Task<BasicMessage>  Run([QueueTrigger("%TriggerQueue%", Connection = "AzureWebJobsStorage")]string requestedEventMessage, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {requestedEventMessage}");

            BasicMessage message = JsonConvert.DeserializeObject<BasicMessage>(requestedEventMessage);


            var requestedEvent = message.Body.ToString().ReadFromJson<ActivationCodeBatchRequestedEvent>();


            var result = await _processor.GenerateCode(requestedEvent.CodeCount);


            IEvent respondedEvent = new ActivationCodeBatchRespondedEvent(Guid.NewGuid().ToString(), nameof(ActivationCodeBatchRespondedEvent), DateTime.UtcNow, result);
            BasicMessage returnMessage = new BasicMessage(message.MessageId, respondedEvent.GetType().AssemblyQualifiedName, respondedEvent);

            return returnMessage;

        }
    }
}
