using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Enrollment.Domain.Queries;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.CheckIn.Function
{
    public class CheckInProcessor : ICheckInProcessor
    {
        private readonly IQueryHandler<CheckInQuery, UserProfile> _queryHandler;

        public CheckInProcessor(IQueryHandler<CheckInQuery, UserProfile> queryHandler)
        {
            _queryHandler = queryHandler;
        }
        public async Task<QueryResult> CheckIn(string email)
        {
            var userProfile= await _queryHandler.Query(new CheckInQuery(email));

            if (userProfile != null)
            {
                return new QueryResult(1, 1, new { Email= userProfile.Email, Name=userProfile.FullName }, true, "1 result found.");
            }
            else
            {
                return new QueryResult(1,1,null,false,"Not Found");
            }
        }
    }
}
