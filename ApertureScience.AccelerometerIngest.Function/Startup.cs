using ApertureScience.AccelerometerIngest.Data;
using ApertureScience.AccelerometerIngest.Domain.Commands;
using ApertureScience.AccelerometerIngest.Domain.Repositories;
using ApertureScience.AccelerometerIngest.Function;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ApertureScience.AccelerometerIngest.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {


            string conStr = Environment.GetEnvironmentVariable("ConStr");
            builder.Services.AddDbContext<AccelerometerDbContext>(options =>
            options.UseSqlServer(conStr));

            builder.Services.AddScoped<ICommandHandler<CreateAccelerometerIngestCommand, CommandResult>, AccelerometerIngestCommandHandler>();
            builder.Services.AddScoped<IAccelerometerIngestProcessor, AccelerometerIngestProcessor>();
            builder.Services.AddScoped<IAccelerometerUnitOfWork, AccelerometerUnitOfWork>();
            builder.Services.AddScoped<IAccelerometerRepositoryAsync, AccelerometerRepositoryAsync>();
            builder.Services.BuildServiceProvider();
        }
        
   }
}
