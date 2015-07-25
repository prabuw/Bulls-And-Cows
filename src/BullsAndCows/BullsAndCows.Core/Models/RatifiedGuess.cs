namespace BullsAndCows.Core.Models
{
    public class RatifiedGuess : GeneratedGuess
    {
        public readonly int Bulls;
        public readonly int Cows;

        public RatifiedGuess(int[] value, int bulls, int cows) : base(value)
        {
            Bulls = bulls;
            Cows = cows;
        }
    }
}