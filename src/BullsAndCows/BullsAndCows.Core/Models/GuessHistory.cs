using System.Collections.Generic;

namespace BullsAndCows.Core.Models
{
    public class GuessHistory
    {   
        private readonly List<RatifiedGuess> _guesses;

        public IReadOnlyList<RatifiedGuess> Guesses
        {
            get { return _guesses.AsReadOnly(); }
        }

        public GeneratedGuess UnratifiedGuess { get; private set; }

        public GuessHistory()
        {
            _guesses = new List<RatifiedGuess>();
        }

        public GeneratedGuess AddGuess(int[] value)
        {
            UnratifiedGuess = new GeneratedGuess(value);
            return UnratifiedGuess;
        }

        public void RatifyGuess(int bulls, int cows)
        {
            _guesses.Add(new RatifiedGuess(UnratifiedGuess.Value, bulls, cows));
            UnratifiedGuess = null;
        }
    }
}
