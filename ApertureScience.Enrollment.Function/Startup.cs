

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
using ApertureScience.Enrollment.Function;
using ApertureScience.Enrollment.Data;
using ApertureScience.Enrollment.Domain.Queries;
using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Enrollment.Domain.Commands;
using ApertureScience.Enrollment.Domain.Repositories;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ApertureScience.Enrollment.Function
{
   
    public class Startup : FunctionsStartup
    {
        
        public override void Configure(IFunctionsHostBuilder builder)
        {


            string conStr = Environment.GetEnvironmentVariable("ConStr");
            builder.Services.AddDbContext<EnrollmentDbContext>(options =>
            options.UseSqlServer(conStr));
            builder.Services.AddScoped<IQueryHandler<GetActivationCodeQuery, ActivationCode>, GetActivationCodeQueryHandler>();
            builder.Services.AddScoped<ICommandHandler<CreateEnrollmentCommand, CommandResult>, CreateEnrollmentCommandHandler>();
            builder.Services.AddScoped<IEnrollmentProcessor, EnrollmentProcessor>();
            builder.Services.AddScoped<IEnrollmentUnitOfWork, EnrollmentUnitOfWork>();
            builder.Services.AddScoped<IEnrollmentRepositoryAsync, EnrollmentRepositoryAsync>();
            builder.Services.AddScoped<IActivationCodeRepositoryAsync, ActivationCodeRepositoryAsync>();
            builder.Services.BuildServiceProvider();
        }
    }
}
