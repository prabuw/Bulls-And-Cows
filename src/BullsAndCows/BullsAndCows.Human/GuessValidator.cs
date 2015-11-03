using System;
using System.Text.RegularExpressions;
using BullsAndCows.Human.Interfaces;

namespace BullsAndCows.Human
{
    public class GuessValidator : IGuessValidator
    {
        public bool Validate(string guess)
        {
            const RegexOptions regexOptions = RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase;

            var isGuessValid = new Regex("^[1-9]{4}$", regexOptions);
            var isThereReptitionWithinTheGuess = new Regex("([1-9]{1})[^\\1]*\\1", regexOptions);

            if (!isGuessValid.IsMatch(guess) || isThereReptitionWithinTheGuess.IsMatch(guess))
            {
                throw new ArgumentException(
                    "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.");
            }

            return true;
        }
    }
}