using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.AccelerometerIngest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngest.Data
{
    public class AccelerometerRepositoryAsync : IAccelerometerRepositoryAsync
    {
        private readonly AccelerometerDbContext _context;
        public AccelerometerRepositoryAsync(AccelerometerDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
        }
        
        public async Task AddRangeAsync(Ingest[] entities)
        {
             await _context.Ingests.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<Ingest>> GetByUserIdAsync(Guid id)
        {
            return await _context.Ingests.Where(x => x.UserId == id).ToListAsync();
        }

        public async Task<IEnumerable<Ingest>> GetByUserIdPagingAsync(Guid userId, int page, int pageSize, long timeStamp)
        {
           return  await _context.Ingests.Where(x => x.UserId == userId && timeStamp < x.TimeStamp).OrderByDescending(x=>x.TimeStamp).ToListAsync();

        }
    }
}
