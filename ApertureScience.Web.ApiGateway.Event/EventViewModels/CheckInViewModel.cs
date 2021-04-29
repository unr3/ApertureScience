using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApertureScience.Web.ApiGateway.Event.ViewModels
{
   public class CheckInViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
