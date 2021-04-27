using ApertureScience.ActivationCodeGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeGenerator.Domain.Repositories
{
  public interface IActivationCodeRepositoryAsync
    {
        Task AddAsync(ActivationCode entity);
        Task AddRangeAsync(ActivationCode[] entities);
        Task<ActivationCode> GetByIdAsync(Guid id);
        Task<ActivationCode> GetByCodeAsync(int code);

    }
}
