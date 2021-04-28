using ApertureScience.Library.Event.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.AccelerometerIngest.Domain.Events
{
  public  class AccelerometerIngestCreatedEvent:IEvent
    {
        public string Id => Guid.NewGuid().ToString();

        public string Name => nameof(AccelerometerIngestCreatedEvent);

        public DateTime DateTimeUTC => DateTime.UtcNow;

        public object PayLoad { get; }

        public AccelerometerIngestCreatedEvent(object payload)
        {
            PayLoad = payload;
        }
    }
}
