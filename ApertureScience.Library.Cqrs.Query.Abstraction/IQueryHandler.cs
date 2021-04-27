using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Library.Cqrs.Query.Abstraction
{
    public interface IQueryHandler<TQuery, TResult>
       where TQuery : IQuery
    {
        Task<TResult> Query(TQuery query);
    }
}
