
using ApertureScience.ActivationCodeGenerator.Data;
using ApertureScience.ActivationCodeGenerator.Function;
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

[assembly: FunctionsStartup(typeof(Startup))]
namespace ApertureScience.ActivationCodeGenerator.Function
{
   
    public class Startup : FunctionsStartup
    {
        
        public override void Configure(IFunctionsHostBuilder builder)
        {



            string conStr = Environment.GetEnvironmentVariable("ConStr");
            builder.Services.AddDbContext<ActivationCodeDbContext>(options =>
            options.UseSqlServer(conStr));
            builder.Services.AddScoped<IQueryHandler<GetByCodeQuery, ActivationCode>, GetByCodeCodeQueryHandler>();
             builder.Services.AddScoped<ICommandHandler<CreateActivationCodeCommand, CommandResult>, ActivationCodeCommandHandler>();
            builder.Services.AddScoped<IActivationCodeProcessor, ActivationCodeProcessor>();
            builder.Services.AddScoped<IActivationCodeUnitOfWork,ActivationCodeUnitOfWork>();
            builder.Services.AddScoped<IActivationCodeRepositoryAsync,ActivationCodeRepositoryAsync>();
            builder.Services.AddScoped<ICodeGenerator<int>, RandomSixDigitIntCodeGenerator>();
             builder.Services.BuildServiceProvider();
        }
    }
}
