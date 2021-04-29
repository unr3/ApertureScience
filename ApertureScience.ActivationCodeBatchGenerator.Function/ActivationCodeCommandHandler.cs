
using ApertureScience.ActivationCodeGenerator.Data;
using ApertureScience.ActivationCodeGenerator.Domain.Commands;
using ApertureScience.ActivationCodeGenerator.Domain.Entities;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeBatchGenerator.Function
{
    public class ActivationCodeCommandHandler : ICommandHandler<CreateActivationCodeBatchCommand, CommandResult>
    {
        private readonly IActivationCodeUnitOfWork _uow;

        public ActivationCodeCommandHandler(IActivationCodeUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CommandResult> Execute(CreateActivationCodeBatchCommand command)
        {
            ActivationCode[] codes = new ActivationCode[command.CodeCount];
            for (int i = 0; i < codes.Length; i++)
            {
                codes[i] = new ActivationCode(command.Codes[i],false);
            }
          

             await _uow.ActivationCodeRepository.AddRangeAsync(codes);

          return  await _uow.SaveChangesAsync() > 0 ?
                new CommandResult(true, "Activation codes are created")
                : 
                new CommandResult(true, "Activation codes are not created") ;
            
            
          
            
        }
    }
}
