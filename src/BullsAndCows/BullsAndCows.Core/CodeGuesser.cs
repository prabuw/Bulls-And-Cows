using System;
using System.Collections.Generic;
using System.Linq;
using BullsAndCows.Core.Interfaces;
using BullsAndCows.Core.Models;

namespace BullsAndCows.Core
{
    public class CodeGuesser : ICodeGuesser
    {
        private List<int> _allowedNumbers;
        public IReadOnlyList<int> AllowedNumbers { get { return _allowedNumbers.ToList().AsReadOnly(); } }

        private readonly IRandomCodeGenerator _codeGenerator;

        public CodeGuesser(IRandomCodeGenerator codeGenerator)
        {
            _allowedNumbers = Enumerable.Range(1, 9).ToList();
            _codeGenerator = codeGenerator;
        }

        public string CreateGuess(GuessHistory guessHistory)
        {
            if (guessHistory.Guesses.Count == 0)
            {
                var rawGuess = _codeGenerator.Generate();
                var generatedGuess = guessHistory.AddGuess(rawGuess);

                return generatedGuess.ToString();
            }

            return String.Empty;
        }

        public void RatifyGuess(GuessHistory guessHistory, int bulls, int cows)
        {
            guessHistory.RatifyGuess(bulls, cows);
        }

        public void RemoveNumbersThatAreNeitherCowsOrBulls(RatifiedGuess ratifiedGuess)
        {
            foreach (var digit in ratifiedGuess.Value)
            {
                _allowedNumbers.Remove(digit);
            }
        }
    }
}