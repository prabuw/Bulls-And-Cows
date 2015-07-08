
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

            return new VerificationResult();
        }
    }
}
