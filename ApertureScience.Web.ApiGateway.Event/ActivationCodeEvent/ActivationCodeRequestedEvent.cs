using ApertureScience.Library.Event.Abstraction;
using System;

namespace ApertureScience.Web.ApiGateway.Event
{
    public class ActivationCodeRequestedEvent:IEvent
    {
        public string Id { get; }

        public string Name { get; }


        public DateTime DateTimeUTC { get; }
        public int CodeCount { get; }
        public bool IsAdmin { get; }

        public ActivationCodeRequestedEvent(string id, string name, DateTime dateTimeUtc, int codeCount,bool isAdmin)
        {
            Id = id;
            Name = name;
            DateTimeUTC = dateTimeUtc;
            CodeCount = codeCount;
            IsAdmin = isAdmin;

        }


    }
}
