
using ApertureScience.Enrollment.Domain.Commands;
using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Enrollment.Domain.Queries;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using ApertureScience.Library.Cqrs.Query.Abstraction;
using ApertureScience.Web.ApiGateway.Event.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Function
{ 
    public class EnrollmentProcessor: IEnrollmentProcessor
    {
        private readonly IQueryHandler<GetActivationCodeQuery, ActivationCode> _queryHandler;
        private readonly ICommandHandler<CreateEnrollmentCommand, CommandResult> _commandHandler;
 

        public EnrollmentProcessor(IQueryHandler<GetActivationCodeQuery, ActivationCode> queryHandler, ICommandHandler<CreateEnrollmentCommand, CommandResult> commandHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        public async Task<CommandResult> Enroll(EnrollmentViewModel enrollment) {



        CommandResult commandResult = null;
        ActivationCode code = await _queryHandler.Query(new GetActivationCodeQuery(enrollment.Code));

        StringBuilder stringBuilder = new StringBuilder();
        if (code != null)
        {
            if (!code.IsUsed && code.IsValid)
            {
             code.UseCode();
             commandResult=  await  _commandHandler.Execute(new CreateEnrollmentCommand(enrollment.FullName,enrollment.Email,enrollment.Password,enrollment.Code,code.IsAdmin));
            }
            else
            {
                if (code.IsUsed)
                {
                    stringBuilder.AppendLine("Code is already used.");
                }
                if (code.ExpirationDateUtc >= DateTime.UtcNow)
                {
                    code.SetNotValid();
                    stringBuilder.AppendLine("Code is not valid.");

                }
                commandResult = new CommandResult(false, stringBuilder.ToString());
            }

        }
        else
        {
            stringBuilder.AppendLine("Code is not found.");
            commandResult = new CommandResult(false, stringBuilder.ToString());
        }

        return commandResult;

    }
}
}
