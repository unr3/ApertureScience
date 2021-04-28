using ApertureScience.AccelerometerIngest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngest.Domain.Repositories
{
   public interface IAccelerometerRepositoryAsync
    {
        Task AddRangeAsync(Ingest[] entities);
        Task<IEnumerable<Ingest>> GetByUserIdAsync(Guid id);
        Task<IEnumerable<Ingest>> GetByUserIdPagingAsync(Guid id,int page,int pageSize,long timeStamp);
    }
}
