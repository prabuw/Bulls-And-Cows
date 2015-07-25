using System;

namespace BullsAndCows.Core.Models
{
    public class GeneratedGuess
    {
        public readonly int[] Value;

        public GeneratedGuess(int[] value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return String.Join(String.Empty, Value);
        }
    }
}