using ApertureScience.ActivationCodeGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.ActivationCodeGenerator.Data
{
   public class ActivationCodeDbContext:DbContext
    {
     
        public ActivationCodeDbContext(DbContextOptions options):base(options)
        {
           
        }

      public  DbSet<ActivationCode> ActivationCodes { get; set; }

    
    }
}
