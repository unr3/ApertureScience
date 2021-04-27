using ApertureScience.ActivationCodeGenerator.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeGenerator.Data
{
   public class ActivationCodeUnitOfWork : IActivationCodeUnitOfWork
    {
        private readonly ActivationCodeDbContext _context;
        public ActivationCodeDbContext Context => _context;
        private readonly IActivationCodeRepositoryAsync _repo;
        public IActivationCodeRepositoryAsync ActivationCodeRepository => _repo;

        public ActivationCodeUnitOfWork(ActivationCodeDbContext context, IActivationCodeRepositoryAsync repo)
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
