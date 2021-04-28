using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.AccelerometerIngest.Domain.Commands
{
   public class CreateAccelerometerIngestCommand:ICommand
    {
        public Ingest[] Data { get; }

        public CreateAccelerometerIngestCommand(Ingest[] data)
        {
            Data = data;
        }
    }
}
