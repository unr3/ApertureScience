using ApertureScience.AccelerometerIngest.Domain.Commands;
using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngest.Function
{
    class AccelerometerIngestProcessor : IAccelerometerIngestProcessor
    {
        private readonly ICommandHandler<CreateAccelerometerIngestCommand, CommandResult> _commandHandler;

        public AccelerometerIngestProcessor(ICommandHandler<CreateAccelerometerIngestCommand, CommandResult> commandHandler)
        {
            _commandHandler = commandHandler;
        }
        public async Task<CommandResult> IngestData(Ingest[] data)
        {
            return await _commandHandler.Execute(new CreateAccelerometerIngestCommand(data));
        }
    }
}
