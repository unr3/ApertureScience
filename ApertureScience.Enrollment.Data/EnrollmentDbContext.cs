using ApertureScience.Enrollment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Enrollment.Data
{
  public  class EnrollmentDbContext:DbContext
    {
        public EnrollmentDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ActivationCode> ActivationCodes { get; set; }
    }
}
