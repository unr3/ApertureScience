using ApertureScience.Library.Event.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Web.ApiGateway.Event
{
   public class AccelerometerIngestUserDataRequestedEvent : IEvent
    {
        public string Id { get; }

        public string Name { get; }


        public DateTime DateTimeUTC { get; }
        public Guid UserId { get; }
        public long TimeStamp { get; }
        public int Page { get; }
        public int PageSize { get; }

        public AccelerometerIngestUserDataRequestedEvent(string id, string name, DateTime dateTimeUtc, Guid userId,long timeStamp,int page,int pageSize)
        {
            Id = id;
            Name = name;
            DateTimeUTC = dateTimeUtc;
            UserId = userId;
            TimeStamp = timeStamp;
            Page = page;
            PageSize = pageSize;

        }
    }
}
