using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.ViewModels
{
    public class AuthenticationModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password
        {
            get; set;
        }
    }
}
