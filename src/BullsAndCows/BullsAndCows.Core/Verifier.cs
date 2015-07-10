using System;
using System.Text.RegularExpressions;

namespace BullsAndCows.Core
{
    public class Verifier : IVerifier
    {
        public VerificationResult Verify(string code, string guess)
        {
            const RegexOptions regexOptions = RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase;

            var isGuessValid = new Regex("^[1-9]{4}$", regexOptions);
            var isThereReptitionWithinTheGuess = new Regex("([1-9]{1})[^\\1]*\\1", regexOptions);

            if (!isGuessValid.IsMatch(guess) || isThereReptitionWithinTheGuess.IsMatch(guess))
            {
                throw new ArgumentException("Your guess is not in the valid format. It should be contain four unique digits, excluding zero.");
            }

            var bulls = 0;
            var cows = 0;

            for (var i = 0; i < guess.Length; i++)
            {
                var index = guess.IndexOf(code[i]);

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
