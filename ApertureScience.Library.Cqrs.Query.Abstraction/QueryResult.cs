using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Library.Cqrs.Query.Abstraction
{
    public class QueryResult
    {
      
        int TotalPageCount { get; }
        int CurrentPage { get; }
        object Result { get; }
        bool Success { get; set; }
        string Message { get;  }

        public QueryResult(int totalPageCount,int currentPage,object result,bool success,string message)
        {
         
            TotalPageCount = totalPageCount;
            CurrentPage = currentPage;
            Result = result;
            Success = success;
            Message = message;
        }

    }
}
