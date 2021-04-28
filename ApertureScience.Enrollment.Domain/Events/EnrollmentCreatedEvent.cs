using ApertureScience.Library.Event.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Enrollment.Domain.Events
{
    public class EnrollmentCreatedEvent : IEvent
    {
        public string Id => Guid.NewGuid().ToString();

        public string Name => nameof(EnrollmentCreatedEvent);

        public DateTime DateTimeUTC => DateTime.UtcNow;

        public object PayLoad { get; }

        public EnrollmentCreatedEvent(object payload)
        {
            PayLoad = payload;
        }
    }
}
