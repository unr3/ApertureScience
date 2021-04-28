using ApertureScience.ActivationCodeGenerator.Data;
using ApertureScience.ActivationCodeGenerator.Domain.Entities;
using ApertureScience.ActivationCodeGenerator.Domain.Queries;
using ApertureScience.ActivationCodeGenerator.Domain.Repositories;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeGenerator.Function
{
    public class GetByCodeCodeQueryHandler : IQueryHandler<GetByCodeQuery, ActivationCode>
    {
        private readonly IActivationCodeUnitOfWork _uow;

        public GetByCodeCodeQueryHandler(IActivationCodeUnitOfWork uow)
        {

                _uow = uow;
            
        }
        public async Task<ActivationCode> Query(GetByCodeQuery query)
        {
            return await _uow.ActivationCodeRepository.GetByCodeAsync(query.Code);
                
        }
    }
}
