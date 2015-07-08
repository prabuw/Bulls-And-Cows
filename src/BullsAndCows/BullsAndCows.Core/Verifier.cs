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
            var isGuessContainRepetition = new Regex("([1-9]{1})[^\\1]*\\1", regexOptions);

            if (!isGuessValid.IsMatch(guess) || isGuessContainRepetition.IsMatch(guess))
            {
                throw new ArgumentException("Your guess is not in the valid format. It should be contain four unique digits, excluding zero.");
            }

            var bulls = 0;
            var cows = 0;

            for (var i = 0; i < guess.Length; i++)
            {
                if (code[i] == guess[i])
                {
                    bulls++;
                }
                else if(guess.IndexOf(code[i]) >= 0)
                {
                    cows++;
                }
            }

            return new VerificationResult(bulls, cows);
        }
    }
}
