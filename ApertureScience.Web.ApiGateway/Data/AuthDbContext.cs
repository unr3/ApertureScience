using ApertureScience.Web.ApiGateway.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Data
{
    public class AuthDbContext:DbContext
    {
        public AuthDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
