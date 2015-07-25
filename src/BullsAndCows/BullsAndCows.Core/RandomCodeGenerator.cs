using System;
using System.Collections.Generic;
using System.Linq;
using BullsAndCows.Core.Interfaces;

namespace BullsAndCows.Core
{
    public class RandomCodeGenerator : IRandomCodeGenerator
    {
        private readonly IEnumerable<int> _allowedNumbers;

        public RandomCodeGenerator()
        {
            _allowedNumbers = Enumerable.Range(1, 9);
        }

        public int[] Generate()
        {
            var seed = new Random();
            var codeAsNumbers = _allowedNumbers.OrderBy(x => seed.Next()).Take(4).ToArray();

            return codeAsNumbers;
        }
    }
}