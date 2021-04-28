using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Messaging.Implementation;
using ApertureScience.Web.ApiGateway.Event;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ApertureScience.Library.Extension;

namespace ApertureScience.ActivationCodeGenerator.Function
{
    public  class ActivationCodeProcess
    {
        private readonly IActivationCodeProcessor _processor;
        public ActivationCodeProcess(IActivationCodeProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("ActivationCodeProcess")]
        [return: Queue("%ResponseQueue%", Connection = "AzureWebJobsStorage")]
        public async Task<BasicMessage> Run([QueueTrigger("%TriggerQueue%", Connection = "AzureWebJobsStorage")]string requestedEventMessage, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {requestedEventMessage}");

            BasicMessage message = JsonConvert.DeserializeObject<BasicMessage>(requestedEventMessage);

            var requestedEvent =message.Body.ToString().ReadFromJson<ActivationCodeRequestedEvent>();
            
            var result= await _processor.GenerateCode(requestedEvent.IsAdmin);


            IEvent respondedEvent = new ActivationCodeRespondedEvent(Guid.NewGuid().ToString(), nameof(ActivationCodeRespondedEvent),DateTime.UtcNow,result);
            BasicMessage returnMessage = new BasicMessage(message.MessageId,respondedEvent.GetType().AssemblyQualifiedName, respondedEvent);

            return returnMessage;
           
        }
      
    }
}
