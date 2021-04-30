using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApertureScience.Web.ApiGateway.ViewModels
{
   public class UserDataViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public long TimeStamp { get; set; }
        [Required]
        public int Page { get; set; }
        
    }
}
