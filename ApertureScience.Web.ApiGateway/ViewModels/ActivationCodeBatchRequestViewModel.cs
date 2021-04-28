using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.ViewModels
{
    public class ActivationCodeBatchRequestViewModel
    {
        [Required]
        public int BatchSize { get; set; }
    }
}
