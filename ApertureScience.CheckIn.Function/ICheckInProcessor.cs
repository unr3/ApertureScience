using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.CheckIn.Function
{
  public interface ICheckInProcessor
    {
        Task<QueryResult> CheckIn(string email);
    }
}
