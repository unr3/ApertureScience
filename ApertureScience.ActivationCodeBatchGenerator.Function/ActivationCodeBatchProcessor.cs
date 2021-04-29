
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

namespace ApertureScience.ActivationCodeBatchGenerator.Function
{
    public class ActivationCodeBatchProcessor: IActivationCodeBatchProcessor
    {
        private readonly IQueryHandler<GetByCodeQuery, ActivationCode> _queryHandler;
        private readonly ICommandHandler<CreateActivationCodeBatchCommand, CommandResult> _commandHandler;
 
        private readonly ICodeGenerator<int> _codeGenerator;

        public ActivationCodeBatchProcessor(IQueryHandler<GetByCodeQuery, ActivationCode> queryHandler, ICommandHandler<CreateActivationCodeBatchCommand, CommandResult> commandHandler, ICodeGenerator<int> codeGenerator)
        {
            _commandHandler = commandHandler;
            _codeGenerator = codeGenerator;
            _queryHandler = queryHandler;
        }

        public async Task<CommandResult> GenerateCode(int codeCount) {
            int attemp = 200;
            int code = 0;
            List<int> codes = new List<int>();
            for (int i = 0; i < codeCount; i++)
            {
                do
                {
                    code = _codeGenerator.Generate();
                    ActivationCode activationCode = await _queryHandler.Query(new GetByCodeQuery(code));

                    if (activationCode == null)
                    {   codes.Add(code);
                        break;
                    }
                       

                } while (attemp <= _codeGenerator.MaxAttempt);
            }

            if(codes.Count!=codeCount)
                return new CommandResult(false, "Codes are not generated");

            return await _commandHandler.Execute(new CreateActivationCodeBatchCommand(codeCount,codes.ToArray()));

            

        }
    }
}
