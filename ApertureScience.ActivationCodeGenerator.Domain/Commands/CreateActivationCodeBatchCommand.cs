using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.ActivationCodeGenerator.Domain.Commands
{
  public  class CreateActivationCodeBatchCommand:ICommand
    {
        public int CodeCount { get; }
        public int[] Codes { get; }
        public CreateActivationCodeBatchCommand(int codeCount,int[] codes)
        {
            CodeCount = codeCount;
            Codes = codes;
        }
    }
}
