using ApertureScience.Library.Cqrs.Command.Abstraction;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeBatchGenerator.Function
{
   public interface IActivationCodeBatchProcessor
    {
        Task<CommandResult> GenerateCode(int codeCount);
    }
}
