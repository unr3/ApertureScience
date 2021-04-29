using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Enrollment.Domain.Queries;
using ApertureScience.Enrollment.Data;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Function
{
    public class GetActivationCodeQueryHandler : IQueryHandler<GetActivationCodeQuery, ActivationCode>
    {
        private readonly IEnrollmentUnitOfWork _uow;

        public GetActivationCodeQueryHandler(IEnrollmentUnitOfWork uow)
        {

            _uow = uow;

        }
        public async Task<ActivationCode> Query(GetActivationCodeQuery query)
        {
            return await _uow.ActivationCodeRepositoryAsync.GetActivationCodeAsync(query.Code);

        }
    }
}
