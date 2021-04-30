
using ApertureScience.AccelerometerIngest.Data;
using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.AccelerometerIngest.Domain.Queries;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngestUserData.Function
{
    class GetByUserIdPagingQueryHandler : IQueryHandler<GetByUserIdPagingQuery, IEnumerable<Ingest>>
    {
        private readonly IAccelerometerUnitOfWork _uow;

        public GetByUserIdPagingQueryHandler(IAccelerometerUnitOfWork uow)
        {

            _uow = uow;

        }
        public async Task<IEnumerable<Ingest>> Query(GetByUserIdPagingQuery query)
        {
            return await _uow.AccelerometerRepositoryAsync.GetByUserIdPagingAsync(query.UserId,query.Page,query.PageSize,query.TimeStamp);

        }
    }
}
