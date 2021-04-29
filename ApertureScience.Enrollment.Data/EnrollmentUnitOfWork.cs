using ApertureScience.Enrollment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Data
{
    public class EnrollmentUnitOfWork : IEnrollmentUnitOfWork
    {
        private readonly EnrollmentDbContext _context;
        public EnrollmentDbContext Context => _context;

        private readonly IEnrollmentRepositoryAsync _enrollmentRepo;
        public IEnrollmentRepositoryAsync EnrollmentRepositoryAsync =>_enrollmentRepo;
        private readonly IActivationCodeRepositoryAsync _activationCodeRepo;
        public IActivationCodeRepositoryAsync ActivationCodeRepositoryAsync =>_activationCodeRepo ;

        public EnrollmentUnitOfWork(EnrollmentDbContext context,IEnrollmentRepositoryAsync enrollmentRepo, IActivationCodeRepositoryAsync activationCodeRepo)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (enrollmentRepo == null)
                throw new ArgumentNullException(nameof(enrollmentRepo));

            if (activationCodeRepo == null)
                throw new ArgumentNullException(nameof(activationCodeRepo));

            _context = context;
            _enrollmentRepo = enrollmentRepo;
            _activationCodeRepo = activationCodeRepo;
            
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
