using ApertureScience.AccelerometerIngest.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngest.Data
{
    public class AccelerometerUnitOfWork : IAccelerometerUnitOfWork
    {
        private readonly AccelerometerDbContext _context;
        public AccelerometerDbContext Context => _context;
        private readonly IAccelerometerRepositoryAsync _repo;
        public IAccelerometerRepositoryAsync AccelerometerRepositoryAsync => _repo;

        public AccelerometerUnitOfWork(AccelerometerDbContext context, IAccelerometerRepositoryAsync repo)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (repo == null)
                throw new ArgumentNullException(nameof(repo));

            _context = context;
            _repo = repo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
