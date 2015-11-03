using System;
using System.Collections.Generic;
using System.Linq;
using BullsAndCows.Human.Interfaces;

namespace BullsAndCows.Human
{
    public class RandomCodeGenerator : IRandomCodeGenerator
    {
        private readonly IEnumerable<int> _allowedNumbers;

        public RandomCodeGenerator()
        {
            _allowedNumbers = Enumerable.Range(1, 9);
        }

        public string Generate()
        {
            var seed = new Random();
            var codeAsNumbers = _allowedNumbers.OrderBy(x => seed.Next()).Take(4).Select(c => c.ToString());
            var code = string.Join(string.Empty, codeAsNumbers);

            return code;
        }
    }
}