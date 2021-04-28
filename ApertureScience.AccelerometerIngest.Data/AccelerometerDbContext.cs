using ApertureScience.AccelerometerIngest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.AccelerometerIngest.Data
{
   public class AccelerometerDbContext:DbContext
    {
        public AccelerometerDbContext(DbContextOptions options):base(options)
        {
           
        }

      public  DbSet<Ingest> Ingests { get; set; }

    
    }
}
