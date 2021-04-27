using ApertureScience.Library.Event.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.ActivationCodeGenerator.Domain.Events
{
    public class ActivationCodeCBatchreatedEvent : IEvent
    {
        public string Id => Guid.NewGuid().ToString();

        public string Name => nameof(ActivationCodeCreatedEvent);

        public DateTime DateTimeUTC => DateTime.UtcNow;

        public object PayLoad { get; }

        public ActivationCodeCBatchreatedEvent(object payload)
        {
            PayLoad = payload;
        }
    }
}
