using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Library.Messaging.Abstraction
{
    public interface IQueueManager
    {

        void CreateClient(string connectionString, string queueName);
        Task<bool> ExistsAsync();
        Task SendAsync(IMessage message);
        Task<IMessage> ReceiveAsync();
        Task<bool> PeekAsync(string messageId);
    }
}
