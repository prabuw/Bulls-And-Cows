namespace BullsAndCows.Core.Models
{
    public class VerificationResult
    {
        public readonly int Bulls;

        public readonly int Cows;

        public VerificationResult(int bulls, int cows)
        {
            Bulls = bulls;
            Cows = cows;
        }
    }
}