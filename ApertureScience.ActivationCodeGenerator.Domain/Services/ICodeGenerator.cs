using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.ActivationCodeGenerator.Domain.Services
{
  
        public interface ICodeGenerator<T>
    {
            int MaxAttempt { get; }
            T Generate();
        }
    
}
