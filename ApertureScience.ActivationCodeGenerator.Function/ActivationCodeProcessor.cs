
using ApertureScience.ActivationCodeGenerator.Data;
using ApertureScience.ActivationCodeGenerator.Domain.Commands;
using ApertureScience.ActivationCodeGenerator.Domain.Entities;
using ApertureScience.ActivationCodeGenerator.Domain.Queries;
using ApertureScience.ActivationCodeGenerator.Domain.Services;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeGenerator.Function
{
    public class ActivationCodeProcessor: IActivationCodeProcessor
    {
        private readonly IQueryHandler<GetByCodeQuery, ActivationCode> _queryHandler;
        private readonly ICommandHandler<CreateActivationCodeCommand, CommandResult> _commandHandler;
 
        private readonly ICodeGenerator<int> _codeGenerator;

        public ActivationCodeProcessor(IQueryHandler<GetByCodeQuery, ActivationCode> queryHandler, ICommandHandler<CreateActivationCodeCommand, CommandResult> commandHandler, ICodeGenerator<int> codeGenerator)
        {
            _commandHandler = commandHandler;
            _codeGenerator = codeGenerator;
            _queryHandler = queryHandler;
        }

        public async Task<CommandResult> GenerateCode(bool isAdmin) {
            int attemp = 200;
            int code = 0;
            do
            {
              code = _codeGenerator.Generate();
                ActivationCode activationCode = await _queryHandler.Query(new GetByCodeQuery(code));

                if (activationCode == null)
                    return await _commandHandler.Execute(new CreateActivationCodeCommand(code,isAdmin));

            } while (attemp<=_codeGenerator.MaxAttempt);


             return new CommandResult(false,"Code is not generated");

        }
    }
}
