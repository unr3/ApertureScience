using ApertureScience.Web.ApiGateway.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Data
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AuthDbContext _context;
        public LoginRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<UserProfile> FindUser(string email)
        {
           return await _context.UserProfiles.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
