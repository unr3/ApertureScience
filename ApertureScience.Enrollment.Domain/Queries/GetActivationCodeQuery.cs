using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Enrollment.Domain.Queries
{
    public class GetActivationCodeQuery:IQuery
    {
        public int Code { get; }

        public GetActivationCodeQuery(int code)
        {
            Code = code;
        }
    }
}
