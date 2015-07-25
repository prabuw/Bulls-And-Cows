using BullsAndCows.Core.Interfaces;
using BullsAndCows.Core.Models;

namespace BullsAndCows.Core
{
    public class Verifier : IVerifier
    {
        public VerificationResult Verify(string code, string rawGuess)
        {
            var guess = new Guess(rawGuess);

            var bulls = 0;
            var cows = 0;

            for (var i = 0; i < guess.Value.Length; i++)
            {
                var index = guess.Value.IndexOf(code[i]);

                if (index == i)
                {
                    bulls++;
                }
                else if (index >= 0)
                {
                    cows++;
                }
            }

            return new VerificationResult(bulls, cows);
        }
    }
}
