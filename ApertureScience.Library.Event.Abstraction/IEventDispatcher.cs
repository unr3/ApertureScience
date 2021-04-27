using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Library.Event.Abstraction
{
   public interface IEventDispatcher
    {
        string QueueConnection { get; }
        string QueueName { get; }
        void SetQueueConnectionAndName(string queueConnection, string queueName);
        Task<string> Dispatch(IEvent @event);
    }
}
