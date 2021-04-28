using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.AccelerometerIngest.Domain.Queries
{
 public   class GetByUserIdQuery:IQuery
    {
        Guid UserId { get; }

        public GetByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
