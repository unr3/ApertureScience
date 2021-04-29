

using ApertureScience.Library.Cqrs.Command.Abstraction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Extensions.Configuration;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using ApertureScience.CheckIn.Function;
using ApertureScience.Enrollment.Data;
using ApertureScience.Enrollment.Domain.Queries;
using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Enrollment.Domain.Commands;
using ApertureScience.Enrollment.Domain.Repositories;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ApertureScience.CheckIn.Function
{
   
    public class Startup : FunctionsStartup
    {
        
        public override void Configure(IFunctionsHostBuilder builder)
        {


            string conStr = Environment.GetEnvironmentVariable("ConStr");
            builder.Services.AddDbContext<EnrollmentDbContext>(options =>
            options.UseSqlServer(conStr));
            builder.Services.AddScoped<IQueryHandler<CheckInQuery, UserProfile>, CheckInQueryHandler>();
           
            builder.Services.AddScoped<ICheckInProcessor, CheckInProcessor>();
            builder.Services.AddScoped<IEnrollmentUnitOfWork, EnrollmentUnitOfWork>();
            builder.Services.AddScoped<IEnrollmentRepositoryAsync, EnrollmentRepositoryAsync>();
            builder.Services.AddScoped<IActivationCodeRepositoryAsync, ActivationCodeRepositoryAsync>();
            builder.Services.BuildServiceProvider();
        }
    }
}
