using System;
using System.ComponentModel.DataAnnotations;

namespace ApertureScience.Web.ApiGateway.Event.ViewModels
{
    public class IngestDataViewModel
    {
        [Required]
        public Guid UserId { get;  set; }
        [Required]
        public int DimensionX { get;  set; }
        [Required]
        public int DimensionY { get;  set; }
        [Required]
        public int DimensionZ { get;  set; }
        [Required]
        public long TimeStamp { get;  set; }
       
    }
}
