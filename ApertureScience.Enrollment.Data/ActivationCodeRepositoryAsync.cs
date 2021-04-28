using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Enrollment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Data
{
    public class ActivationCodeRepositoryAsync : IActivationCodeRepositoryAsync
    {
        private readonly EnrollmentDbContext _context;
        public ActivationCodeRepositoryAsync(EnrollmentDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
        }
        public async Task<ActivationCode> GetActivationCodeAsync(int code)
        {
            return await _context.ActivationCodes.FirstOrDefaultAsync(x=>x.Code==code);
        }

       
       
    }
}
