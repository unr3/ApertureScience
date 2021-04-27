using ApertureScience.Library.Event.Abstraction;
using System;

namespace ApertureScience.Web.ApiGateway.Event
{
    public class ActivationCodeBatchRespondedEvent : IEvent
    {
        public string Id { get; }

        public string Name { get; }

        public DateTime DateTimeUTC { get; }
      
        public object Result { get; }

        public ActivationCodeBatchRespondedEvent(string id, string name, DateTime dateTimeUtc,object result)
        {
            Id = id;
            Name = name;
            DateTimeUTC = dateTimeUtc;
            Result = result;

        }


    }
}
