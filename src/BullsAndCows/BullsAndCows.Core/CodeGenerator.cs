using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows.Core
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly IEnumerable<int> _allowedNumbers;

        public CodeGenerator()
        {
            _allowedNumbers = Enumerable.Range(1, 9);
        }

        public string Generate()
        {
            var seed = new Random();
            var codeAsNumbers = _allowedNumbers.OrderBy(x => seed.Next()).Take(4);

            return String.Join(String.Empty, codeAsNumbers);
        }
    }
}