using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Library.Cqrs.Query.Abstraction
{
    public class QueryResult
    {
      
        public int TotalPageCount { get; }
        public int CurrentPage { get; }
        public object Result { get; }
        public bool Success { get; set; }
        public string Message { get;  }

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
