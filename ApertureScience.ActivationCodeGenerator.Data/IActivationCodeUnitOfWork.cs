using ApertureScience.ActivationCodeGenerator.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeGenerator.Data
{
   public interface IActivationCodeUnitOfWork:IDisposable
    {
        IActivationCodeRepositoryAsync ActivationCodeRepository { get; }
    
        Task<int> SaveChangesAsync();
    }
}
