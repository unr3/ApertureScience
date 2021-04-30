using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngest.Function
{
   public interface IAccelerometerIngestProcessor
    {
        Task<CommandResult> IngestData(Ingest[] data);
    }
}
