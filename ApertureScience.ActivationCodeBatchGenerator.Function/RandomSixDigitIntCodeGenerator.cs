using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureScience.ActivationCodeGenerator.Domain.Services;

namespace ApertureScience.ActivationCodeBatchGenerator.Function
{
    public class RandomSixDigitIntCodeGenerator : ICodeGenerator<int>
    {
        public int MaxAttempt => 20;

       

        public int Generate()
        {
            Random generator = new Random();
            String r = generator.Next(100000, 999999).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = Generate().ToString();
            }
            return int.Parse(r);
        }

        
    }
}
