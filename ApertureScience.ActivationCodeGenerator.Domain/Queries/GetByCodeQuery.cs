using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.ActivationCodeGenerator.Domain.Queries
{
   public class GetByCodeQuery:IQuery
    {
        public int Code { get; }

        public GetByCodeQuery(int code)
        {
            Code = code;
        }
    }
}
