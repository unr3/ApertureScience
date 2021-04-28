using ApertureScience.Enrollment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Data
{
    public interface IEnrollmentUnitOfWork : IDisposable
    {
        IEnrollmentRepositoryAsync EnrollmentRepositoryAsync { get; }
        IActivationCodeRepositoryAsync ActivationCodeRepositoryAsync { get; }
        EnrollmentDbContext Context { get; }

        Task<int> SaveChangesAsync();
    }
}
