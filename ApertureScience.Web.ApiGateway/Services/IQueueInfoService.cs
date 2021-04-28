using ApertureScience.Web.ApiGateway.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Services
{
   public interface IQueueInfoService
    {
        QueueInfoViewModel GetQueueInfo(string eventName);
    }
}
