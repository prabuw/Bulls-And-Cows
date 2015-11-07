namespace BullsAndCows.Human.Models
{
    public class VerificationResult
    {
        public readonly string Value;
        public readonly int Bulls;
        public readonly int Cows;

        public VerificationResult(string value, int bulls, int cows)
        {
            Value = value;
            Bulls = bulls;
            Cows = cows;
        }
    }
}