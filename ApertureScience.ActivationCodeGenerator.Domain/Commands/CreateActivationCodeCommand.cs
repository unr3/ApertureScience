using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.ActivationCodeGenerator.Domain.Commands
{
  public   class CreateActivationCodeCommand:ICommand
    {
        public int CodeCount { get; }
        public int Code { get; }
        public bool IsAdmin { get; }


        public CreateActivationCodeCommand(int code,bool isAdmin)
        {
            CodeCount = 1;
            Code = code;
            IsAdmin = isAdmin;
        }
    }
}
