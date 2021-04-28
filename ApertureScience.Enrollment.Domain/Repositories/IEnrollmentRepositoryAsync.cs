using ApertureScience.Enrollment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Domain.Repositories
{
  public  interface IEnrollmentRepositoryAsync
    {
       
        Task Add(UserProfile profile);
        Task<UserProfile> GetUserByEmail(string email);
    }
}
