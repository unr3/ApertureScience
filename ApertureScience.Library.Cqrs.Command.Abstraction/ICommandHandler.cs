using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.Library.Cqrs.Command.Abstraction
{
    public interface ICommandHandler<TCommand,TResult> where TCommand : ICommand
    {
        Task<TResult> Execute(TCommand command);
    }
}
