using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.ViewModels
{
    public class ActivationCodeResponseViewModel
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string ResponseMessage { get; set; }
        public string MessageId { get; set; }
    }
}
