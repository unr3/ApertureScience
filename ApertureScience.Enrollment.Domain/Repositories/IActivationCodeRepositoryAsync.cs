using ApertureScience.Enrollment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Domain.Repositories
{
  public  interface IActivationCodeRepositoryAsync
    {
        Task<ActivationCode> GetActivationCodeAsync(int code);
      
    }
}
