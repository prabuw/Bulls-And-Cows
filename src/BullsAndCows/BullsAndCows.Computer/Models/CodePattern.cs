using System;
using System.Collections.Generic;

namespace BullsAndCows.Computer.Models
{
    public class CodePattern
    {
        public IReadOnlyList<int?> Pattern => _pattern.AsReadOnly();
        private readonly List<int?> _pattern;

        public CodePattern(List<int?> pattern)
        {
            _pattern = pattern;
        }

        public bool IsMatch(IReadOnlyList<int> code)
        {
            var codeLength = code.Count;
            if (codeLength != _pattern.Count)
            {
                throw new ArgumentException();
            }

            for (var i = 0; i < codeLength; i++)
            {
                if (_pattern[i].HasValue && _pattern[i].Value != code[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int? this[int i] => _pattern[i];
    }
}
