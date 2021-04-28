using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.AccelerometerIngest.Domain.Queries
{
 public   class GetByUserIdPagingQuery:IQuery
    {
       public Guid UserId { get; }
       public long TimeStamp { get; }
      public int Page { get; set; }
        public int PageSize { get; set; }

        public GetByUserIdPagingQuery(Guid userId,long timeStamp,int page,int pageSize)
        {
            UserId = userId;
            TimeStamp = TimeStamp;
            Page = page;
            PageSize = pageSize;
        }
    }
}
