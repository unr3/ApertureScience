using ApertureScience.AccelerometerIngest.Data;
using ApertureScience.AccelerometerIngest.Domain.Commands;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngest.Function
{
    public class AccelerometerIngestCommandHandler : ICommandHandler<CreateAccelerometerIngestCommand, CommandResult>
    {
        private readonly IAccelerometerUnitOfWork _uow;

        public AccelerometerIngestCommandHandler(IAccelerometerUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<CommandResult> Execute(CreateAccelerometerIngestCommand command)
        {
            await _uow.AccelerometerRepositoryAsync.AddRangeAsync(command.Data);

            return await _uow.SaveChangesAsync() > 0 ?
                new CommandResult(true, "Data is ingested")
                :
                new CommandResult(true, "Data is not ingested");
            
        }
    }
}
