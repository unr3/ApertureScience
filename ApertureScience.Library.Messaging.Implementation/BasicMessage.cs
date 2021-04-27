

using ApertureScience.Library.Messaging.Abstraction;
using System;
using System.IO;

namespace ApertureScience.Library.Messaging.Implementation
{
    public class BasicMessage : IMessage
    {

        private string _messageId;
        public string MessageId => _messageId;

        private object _body;
        public object Body => _body;

        private string _messageType;
        public string MessageType => _messageType;

     

        public TBody BodyAs<TBody>()
        {
            return (TBody)Body;
        }
       
        public BasicMessage(string messageId, string messageType, object body)
        {
            _messageId = messageId;
            _messageType = messageType;
            _body = body;

        }





        //public static BasicMessage FromJson(Stream jsonStream)
        //{
        //    var message = jsonStream.ReadFromJson<BasicMessage>();
        //    //the body is a JObject at this point - deserialize to the real message type:
        //    message.SetBodyAndMessageType(message.Body.ToString().ReadFromJson(message.MessageType));
        //    return message;
        //}

        //public static BasicMessage FromJson(string json)
        //{


        //    var message = json.ReadFromJson<BasicMessage>();
        //    //the body is a JObject at this point - deserialize to the real message type:
        //    message.SetBodyAndMessageType(message.Body.ToString().ReadFromJson(message.MessageType));
        //    return message;
        //}


    }
}
