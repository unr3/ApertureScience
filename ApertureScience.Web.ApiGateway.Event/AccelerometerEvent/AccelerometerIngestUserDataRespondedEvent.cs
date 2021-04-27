using ApertureScience.Library.Event.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Web.ApiGateway.Event
{
   public class AccelerometerIngestUserDataRespondedEvent : IEvent
    {
        public string Id { get; }

        public string Name { get; }


        public DateTime DateTimeUTC { get; }
        public object Result { get; }

        public AccelerometerIngestUserDataRespondedEvent(string id, string name, DateTime dateTimeUtc, object result)
        {
            Id = id;
            Name = name;
            DateTimeUTC = dateTimeUtc;
            Result = result;

        }
    }
}
