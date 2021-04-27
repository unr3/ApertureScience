using ApertureScience.ActivationCodeGenerator.Domain.Entities;
using ApertureScience.ActivationCodeGenerator.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeGenerator.Data
{
    public class ActivationCodeRepositoryAsync : IActivationCodeRepositoryAsync
    {
       private readonly ActivationCodeDbContext _context;
        public ActivationCodeRepositoryAsync(ActivationCodeDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
        }
        public async Task AddAsync(ActivationCode entity)
        {
            await _context.ActivationCodes.AddAsync(entity);
           
        }

        public async Task AddRangeAsync(ActivationCode[] entities)
        {
            await _context.ActivationCodes.AddRangeAsync(entities);
           
        }
        public async Task<ActivationCode> GetByIdAsync(Guid id)
        {
            return await _context.ActivationCodes.FindAsync(id);
        }
        public async Task<ActivationCode> GetByCodeAsync(int code)
        {
            return await _context.ActivationCodes.FirstOrDefaultAsync(c => c.Code == code);
        }
    }
}
