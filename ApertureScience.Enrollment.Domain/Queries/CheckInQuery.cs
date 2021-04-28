using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Enrollment.Domain.Queries
{
   public class CheckInQuery : IQuery
    {
        public string Email { get; }

        public CheckInQuery(string email)
        {
            Email = email;
        }
    }
}
