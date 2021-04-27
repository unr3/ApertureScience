using ApertureScience.Library.Event.Abstraction;
using System;

namespace ApertureScience.Web.ApiGateway.Event
{
    public class ActivationCodeRespondedEvent : IEvent
    {
        public string Id { get; }

        public string Name { get; }

        public DateTime DateTimeUTC { get; }
      
        public object Result { get; }

        public ActivationCodeRespondedEvent(string id, string name, DateTime dateTimeUtc, object result)
        {
            Id = id;
            Name = name;
            DateTimeUTC = dateTimeUtc;
            Result = result;

        }


    }
}
