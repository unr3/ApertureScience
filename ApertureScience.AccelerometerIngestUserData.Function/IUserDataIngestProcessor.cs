using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngestUserData.Function
{
   public interface IUserDataIngestProcessor
    {
        public Task<QueryResult> GetUserIngestData(Guid userId, long timeStamp, int page, int pageSize);
    }
}
