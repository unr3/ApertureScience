using ApertureScience.AccelerometerIngest.Data;
using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.AccelerometerIngest.Domain.Queries;
using ApertureScience.AccelerometerIngest.Domain.Repositories;
using ApertureScience.AccelerometerIngestUserData.Function;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ApertureScience.AccelerometerIngestUserData.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {


            string conStr = Environment.GetEnvironmentVariable("ConStr");
            builder.Services.AddDbContext<AccelerometerDbContext>(options =>
            options.UseSqlServer(conStr));

            builder.Services.AddScoped<IQueryHandler<GetByUserIdPagingQuery, IEnumerable<Ingest>>, GetByUserIdPagingQueryHandler>();
            builder.Services.AddScoped<IUserDataIngestProcessor, UserDataIngestProcessor>();
            builder.Services.AddScoped<IAccelerometerUnitOfWork, AccelerometerUnitOfWork>();
            builder.Services.AddScoped<IAccelerometerRepositoryAsync, AccelerometerRepositoryAsync>();
            builder.Services.BuildServiceProvider();
        }
        
   }
}
