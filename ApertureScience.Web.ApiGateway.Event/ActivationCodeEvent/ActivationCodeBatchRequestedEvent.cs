using ApertureScience.Library.Event.Abstraction;
using System;

namespace ApertureScience.Web.ApiGateway.Event
{
    public class ActivationCodeBatchRequestedEvent:IEvent
    {
        public string Id { get; }

        public string Name { get; }


        public DateTime DateTimeUTC { get; }
        public int CodeCount { get; }
       
        public ActivationCodeBatchRequestedEvent(string id,string name, DateTime dateTimeUtc, int codeCount)
        {
            Id = id;
            Name = name;
            DateTimeUTC = dateTimeUtc;
            CodeCount = codeCount;
           
        }

      
    }
}
