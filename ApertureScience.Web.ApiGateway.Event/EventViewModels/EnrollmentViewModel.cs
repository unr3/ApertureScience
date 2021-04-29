using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApertureScience.Web.ApiGateway.Event.ViewModels
{
  public class EnrollmentViewModel
    {
        [Required]
        public string FullName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Code { get; set; }

      
    }
}
