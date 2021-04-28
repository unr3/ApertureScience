
using ApertureScience.Library.Messaging.Abstraction;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Library.Messaging.Implementation
{
    public class BasicStorageQueueManager : IQueueManager
    {
        private  QueueClient _queueClient=null;

        public void CreateClient(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
        }


        public async Task<bool> ExistsAsync()
        {
            return await _queueClient.ExistsAsync();            
        }

        public async Task<bool> PeekAsync(string messageId)
        {
            bool result = false;
            if (await ExistsAsync())
            {
                var peeked = await _queueClient.PeekMessageAsync();
                if (peeked != null && peeked.Value != null)
                {
                    IMessage message = JsonConvert.DeserializeObject<BasicMessage>(peeked.Value.Body.ToString());
                    result = message.MessageId == messageId;
                }
            }
            else
            {
                throw new Exception("Queue is not found");
            }
            return result;
        }

        public async Task<IMessage> ReceiveAsync()
        {
           
            if(await ExistsAsync())
            {

                QueueProperties properties = await _queueClient.GetPropertiesAsync();

                if (properties.ApproximateMessagesCount > 0)
                {
                    var message
                        = await _queueClient.ReceiveMessageAsync();
                    return  JsonConvert.DeserializeObject<BasicMessage>(message.Value.Body.ToString()); ;
                }
                else
                {
                    throw new Exception("Message is not found");
                }

            }
            else
            {
                throw new Exception("Queue is not found");
            }

            
           
            
           
        }

        public async Task SendAsync(IMessage message)
        {
           
            
            if (await ExistsAsync())
            {
                string json = JsonConvert.SerializeObject(message) ;
                // Send a message to the queue
               await _queueClient.SendMessageAsync(json);
                
            }
            else
            {
                throw new Exception("Queue is not found");
            }
        }
    }
}
