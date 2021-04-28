
using ApertureScience.ActivationCodeGenerator.Data;
using ApertureScience.ActivationCodeGenerator.Domain.Commands;
using ApertureScience.ActivationCodeGenerator.Domain.Entities;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeGenerator.Function
{
    public class ActivationCodeCommandHandler : ICommandHandler<CreateActivationCodeCommand, CommandResult>
    {
        private readonly IActivationCodeUnitOfWork _uow;

        public ActivationCodeCommandHandler(IActivationCodeUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CommandResult> Execute(CreateActivationCodeCommand command)
        {
            ActivationCode code = new ActivationCode(command.Code,command.IsAdmin);

             await _uow.ActivationCodeRepository.AddAsync(code);
            int result= await _uow.SaveChangesAsync();
            if (result > 0)
            {
                return new CommandResult(true, "Activation code is created");
            }
            else
            {
                return new CommandResult(true, "Activation code is not created");
            }
            
        }
    }
}
