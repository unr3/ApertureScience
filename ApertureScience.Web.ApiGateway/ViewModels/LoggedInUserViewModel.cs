using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.ViewModels
{
    public class LoggedInUserViewModel
    {
       
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}
