using ApertureScience.Library.Event.Abstraction;
using ApertureScience.Library.Messaging.Abstraction;
using ApertureScience.Library.Messaging.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IQueueManager _queueManager;

        private string _queueConnection;
        public string QueueConnection => _queueConnection;

        private string _queueName;
        public string QueueName => _queueName;

        public EventDispatcher(IQueueManager queueManager)
        {
            _queueManager = queueManager;
        }

        public async Task<string> Dispatch(IEvent @event)
        {
            BasicMessage message = new BasicMessage(Guid.NewGuid().ToString(), @event.GetType().AssemblyQualifiedName, @event);
            _queueManager.CreateClient(@QueueConnection, QueueName);
            await _queueManager.SendAsync(message);
            return message.MessageId;
        }

        public void SetQueueConnectionAndName(string queueConnection, string queueName)
        {
            _queueConnection = queueConnection;
            _queueName = queueName;
        }
    }
}
