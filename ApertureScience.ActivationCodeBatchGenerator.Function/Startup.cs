
using ApertureScience.ActivationCodeGenerator.Data;
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
using ApertureScience.ActivationCodeGenerator.Domain.Queries;
using ApertureScience.ActivationCodeGenerator.Domain.Entities;
using ApertureScience.ActivationCodeGenerator.Domain.Commands;
using ApertureScience.ActivationCodeGenerator.Domain.Repositories;
using ApertureScience.ActivationCodeGenerator.Domain.Services;
using ApertureScience.ActivationCodeBatchGenerator.Function;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ApertureScience.ActivationCodeBatchGenerator.Function
{
   
    public class Startup : FunctionsStartup
    {
        
        public override void Configure(IFunctionsHostBuilder builder)
        {


            string conStr = Environment.GetEnvironmentVariable("ConStr");
            builder.Services.AddDbContext<ActivationCodeDbContext>(options =>
            options.UseSqlServer(conStr));
            builder.Services.AddScoped<IQueryHandler<GetByCodeQuery, ActivationCode>, GetByCodeCodeQueryHandler>();
             builder.Services.AddScoped<ICommandHandler<CreateActivationCodeBatchCommand, CommandResult>, ActivationCodeCommandHandler>();
            builder.Services.AddScoped<IActivationCodeBatchProcessor, ActivationCodeBatchProcessor>();
            builder.Services.AddScoped<IActivationCodeUnitOfWork,ActivationCodeUnitOfWork>();
            builder.Services.AddScoped<IActivationCodeRepositoryAsync,ActivationCodeRepositoryAsync>();
            builder.Services.AddScoped<ICodeGenerator<int>, RandomSixDigitIntCodeGenerator>();
             builder.Services.BuildServiceProvider();
        }
    }
}
