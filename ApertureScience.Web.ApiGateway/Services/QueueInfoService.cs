using ApertureScience.Web.ApiGateway.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Services
{
    public class QueueInfoService : IQueueInfoService
    {
        private readonly IConfiguration _configuration;
        public QueueInfoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public QueueInfoViewModel GetQueueInfo(string eventName)
        {
            return _configuration.GetSection(eventName).Get<QueueInfoViewModel>();
        }
    }
}
