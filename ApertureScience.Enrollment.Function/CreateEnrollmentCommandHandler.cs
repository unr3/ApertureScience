using ApertureScience.Enrollment.Data;
using ApertureScience.Enrollment.Domain.Commands;
using ApertureScience.Enrollment.Domain.Entities;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Enrollment.Function
{
    public class CreateEnrollmentCommandHandler : ICommandHandler<CreateEnrollmentCommand, CommandResult>
    {
        private readonly IEnrollmentUnitOfWork _uow;

    public CreateEnrollmentCommandHandler(IEnrollmentUnitOfWork uow)
    {
        _uow = uow;
            
    }

        public async Task<CommandResult> Execute(CreateEnrollmentCommand command)
        {


            UserProfile profile = new UserProfile(Guid.NewGuid(), command.FullName, command.Email, command.Password, command.Code, command.IsAdmin);
            

            await _uow.EnrollmentRepositoryAsync.Add(profile);

            int result = await _uow.SaveChangesAsync();
            if (result == 2)
            {
                return new CommandResult(true, "Enrollment  is created");
            }
            else
            {
                return  new CommandResult(true, "Enrollment is not created");
            }


         



        }
    }
}
