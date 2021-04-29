using ApertureScience.Library.Cqrs.Command.Abstraction;
using ApertureScience.Web.ApiGateway.Event.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Function
{
   public interface IEnrollmentProcessor
    {
        public Task<CommandResult> Enroll(EnrollmentViewModel enrollment);
    }
}
