using ApertureScience.Enrollment.Data;
using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Enrollment.Domain.Queries;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.CheckIn.Function
{
    public class CheckInQueryHandler : IQueryHandler<CheckInQuery, UserProfile>
    {
        private readonly IEnrollmentUnitOfWork _uow;
        public CheckInQueryHandler(IEnrollmentUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<UserProfile> Query(CheckInQuery query)
        {
           return await _uow.EnrollmentRepositoryAsync.GetUserByEmail(query.Email);
        }
    }
}
