using ApertureScience.Library.Event.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Web.ApiGateway.Event
{
   public class AccelerometerIngestRespondedEvent:IEvent
    {
        public string Id { get; }

        public string Name { get; }


        public DateTime DateTimeUTC { get; }
        public object PayLoad { get; }

        public AccelerometerIngestRespondedEvent(string id, string name, DateTime dateTimeUtc, object payLoad)
        {
            Id = id;
            Name = name;
            DateTimeUTC = dateTimeUtc;
            PayLoad = payLoad;

        }
    }
}
