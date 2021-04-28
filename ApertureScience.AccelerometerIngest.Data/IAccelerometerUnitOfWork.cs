using ApertureScience.AccelerometerIngest.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngest.Data
{
   public interface IAccelerometerUnitOfWork:IDisposable
    {
        IAccelerometerRepositoryAsync AccelerometerRepositoryAsync { get; }
    
        Task<int> SaveChangesAsync();
    }
}
