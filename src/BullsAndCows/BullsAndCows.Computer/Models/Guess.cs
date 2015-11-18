using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows.Computer.Models
{
    public class Guess
    {
        public readonly IReadOnlyList<int> Value;
        public int Bulls { get; private set; }
        public int Cows { get; private set; }

        public Guess(IReadOnlyList<int> value)
        {
            Value = value;
        }

        public void AddFeedback(string rawFeedback)
        {
            if (rawFeedback.Length > 2 ||
                Char.IsDigit(rawFeedback[0]) == false ||
                Char.IsDigit(rawFeedback[1]) == false)
            {
                throw new ArgumentException();
            }

            Bulls = (int)Char.GetNumericValue(rawFeedback[0]);
            Cows = (int)Char.GetNumericValue(rawFeedback[1]);
        }

        public int this[int i] => Value[i];

        public override string ToString()
        {
            return String.Join(String.Empty, Value.Select(v => v.ToString()));
        }
    }
}