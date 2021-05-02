using ApertureScience.Web.ApiGateway.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Data
{
   public interface ILoginRepository
    {
        Task<UserProfile> FindUser(string email);
    }
}
