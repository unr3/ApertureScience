using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.AccelerometerIngest.Domain.Queries;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngestUserData.Function
{
   public class UserDataIngestProcessor : IUserDataIngestProcessor
    {
        private readonly IQueryHandler<GetByUserIdPagingQuery, IEnumerable<Ingest>> _queryHandler;


        public UserDataIngestProcessor(IQueryHandler<GetByUserIdPagingQuery, IEnumerable<Ingest>> queryHandler)
        {
            
            _queryHandler = queryHandler;
        }
        public async Task<QueryResult> GetUserIngestData(Guid userId, long timeStamp, int page, int pageSize)
        {
           IEnumerable<Ingest> data=  await _queryHandler.Query(new GetByUserIdPagingQuery(userId, timeStamp, page, pageSize));

            
            if (data.Count()==0)
            {
                return new QueryResult(0, page, null, false, "Data is not found");
            }
            else
            {
                int totalPage = (data.Count() + pageSize - 1) / pageSize;

                var pagingList = data.Skip(page - 1 * pageSize).Take(pageSize);
                return new QueryResult(totalPage, page, pagingList, true, $"{pagingList.Count()} of {data.Count()} sent.");
            }
        }
    }
}
