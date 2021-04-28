using ApertureScience.Library.Cqrs.Command.Abstraction;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.ActivationCodeGenerator.Function
{
   public interface IActivationCodeProcessor
    {
        Task<CommandResult> GenerateCode(bool isAdmin);
    }
}
