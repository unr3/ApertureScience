﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.ViewModels
{
    public class ActivationCodeRequestViewModel
    {
        [Required]
        public bool IsAdmin { get; set; }
    }
}
