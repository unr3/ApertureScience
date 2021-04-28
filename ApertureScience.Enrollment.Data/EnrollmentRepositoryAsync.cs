using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Enrollment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Data
{
    public class EnrollmentRepositoryAsync : IEnrollmentRepositoryAsync
    {
        private readonly EnrollmentDbContext _context;
        public EnrollmentRepositoryAsync(EnrollmentDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
        }
        public async Task Add(UserProfile profile)
        {
           await _context.UserProfiles.AddAsync(profile);
        }

        public async Task<UserProfile> GetUserByEmail(string email)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(u=>u.Email==email);
        }
    }
}
