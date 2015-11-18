using System;
using System.Collections.Generic;
using System.Linq;
using BullsAndCows.Computer.Interfaces;
using BullsAndCows.Computer.Models;
using Combinatorics.Collections;

namespace BullsAndCows.Computer
{
    public class GuessGenerator : IGuessGenerator
    {
        private static readonly List<int> Source = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private List<IReadOnlyList<int>> _allCodes { get; set; }

        private readonly List<Guess> _guessHistory;
        public IReadOnlyList<Guess> GuessHistory => _guessHistory.AsReadOnly();

        private bool _isFirstGuess;
        private bool _fourDigitsFound;
        private List<int> _invalidDigits = new List<int>();

        public GuessGenerator()
        {
            BuildPermuations();

            _guessHistory = new List<Guess>();
            _isFirstGuess = true;
            _fourDigitsFound = false;
        }

        private void BuildPermuations()
        {
            var permuations = new Variations<int>(Source, 4, GenerateOption.WithoutRepetition);

            _allCodes = new List<IReadOnlyList<int>>();
            foreach (var permuation in permuations)
            {
                _allCodes.Add(permuation.ToList().AsReadOnly());
            }
        }

        public Guess Generate()
        {
            IReadOnlyList<int> code;

            if (_isFirstGuess)
            {
                code = _allCodes.First();
                _isFirstGuess = false;
            }
            else
            {
                var random = new Random();
                var index = random.Next(0, _allCodes.Count);

                code = _allCodes[index];
            }

            _allCodes.Remove(code);
            return new Guess(code);
        }

        public void Process(Guess guess)
        {
            _guessHistory.Add(guess);

            if (guess.Bulls == 0 && guess.Cows == 0)
            {
                AddInvalidDigits(guess.Value);
                ClearCombinationsWithInvalidNumbers();
            }
            else if ((guess.Bulls + guess.Cows == 4) && !_fourDigitsFound)
            {
                _fourDigitsFound = true;

                AddInvalidDigits(Source.Except(guess.Value));
                ClearCombinationsWithInvalidNumbers();
            }
            else
            {
                Func<IReadOnlyList<int>, bool> isMatch;
                if (guess.Cows == 0)
                {
                    var patterns = BuildPatternList(guess, guess.Bulls);

                    isMatch = code =>
                    {
                        return patterns.Any(p => p.IsMatch(code)) == false;
                    };

                    if (patterns.Any())
                        RemoveInvalidCode(isMatch);
                }
                else if (guess.Bulls == 0)
                {
                    var patterns = BuildPatternList(guess, guess.Cows);

                    isMatch = code =>
                    {
                        return patterns.Any(p => p.IsMatch(code));
                    };

                    if (patterns.Any())
                        RemoveInvalidCode(isMatch);
                }
                else
                {
                    var bullPatterns = BuildPatternList(guess, guess.Bulls);
                    var patterns = FillInPatterns(guess, bullPatterns);

                    isMatch = code =>
                    {
                        return patterns.Any(p => p.IsMatch(code)) == false;
                    };

                    if (patterns.Any())
                        RemoveInvalidCode(isMatch);
                }
            }
        }

        private List<CodePattern> FillInPatterns(Guess guess, List<CodePattern> bullPatterns)
        {
            var patterns = new List<CodePattern>();

            foreach (var bullPattern in bullPatterns)
            {
                var potentialCows = new List<DigitWithPosition>();

                for (var i = 0; i < bullPattern.Pattern.Count; i++)
                {
                    if (bullPattern[i].HasValue == false)
                    {
                        potentialCows.Add(new DigitWithPosition(guess[i], i));
                    }
                }

                var cowCombinations = CreateCowCombinations(guess, potentialCows);

                var appliedPatterns = ApplyCowCombinations(bullPattern, cowCombinations);
                patterns.AddRange(appliedPatterns);
            }

            return patterns;
        }

        private List<IList<DigitWithPosition>> CreateCowCombinations(Guess guess, List<DigitWithPosition> potentialCows)
        {
            var cows = new List<DigitWithPosition>();

            foreach (var potentialCow in potentialCows)
            {
                var otherPositions = potentialCows
                    .Where(c => c.Digit != potentialCow.Digit)
                    .Select(c => c.Position);

                cows.AddRange(otherPositions.Select(p => new DigitWithPosition(potentialCow.Digit, p)));
            }

            var cowCombinations =new Combinations<DigitWithPosition>(cows, guess.Cows, GenerateOption.WithoutRepetition).ToList();
            StripOutInvalidCowCombinations(cowCombinations);
            return cowCombinations;
        }

        private List<CodePattern> ApplyCowCombinations(CodePattern bullPattern, List<IList<DigitWithPosition>> cowCombinations)
        {
            var basePattern = bullPattern.Pattern.ToList();
            var patterns = new List<CodePattern>();

            foreach (var cowCombination in cowCombinations)
            {
                var appliedRawPattern = new List<int?>(basePattern);

                foreach (var digitWithPosition in cowCombination)
                {
                    if(appliedRawPattern[digitWithPosition.Position].HasValue)
                        throw new InvalidOperationException();

                    appliedRawPattern[digitWithPosition.Position] = digitWithPosition.Digit;
                }

                patterns.Add(new CodePattern(appliedRawPattern));
            }

            return patterns;
        }

        private void StripOutInvalidCowCombinations(List<IList<DigitWithPosition>> cowCombinations)
        {
            for (var i = cowCombinations.Count() - 1; i >= 0; i--)
            {
                var cowCombination = cowCombinations[i];
                var digits = cowCombination.Select(c => c.Digit);
                var positions = cowCombination.Select(c => c.Position);

                if (digits.Count() != digits.Distinct().Count() || positions.Count() != positions.Distinct().Count())
                {
                    cowCombinations.RemoveAt(i);
                }
            }
        }

        private List<CodePattern> BuildPatternList(Guess guess, int lowerIndex)
        {
            var patterns = new List<CodePattern>();
            var basePattern = new List<int?>() { guess[0], guess[1], guess[2], guess[3] };
            var positions = Enumerable.Range(0, guess.Value.Count).ToList();

            var variations = new Combinations<int>(positions, lowerIndex, GenerateOption.WithoutRepetition);

            foreach (var variation in variations)
            {
                var basePatternCopy = new List<int?>(basePattern);

                for (var i = 0; i < basePatternCopy.Count; i++)
                {
                    if (variation.Contains(i) == false)
                    {
                        basePatternCopy[i] = null;
                    }
                }

                patterns.Add(new CodePattern(basePatternCopy));
            }

            return patterns;
        }

        private void ClearCombinationsWithInvalidNumbers()
        {
            Func<IReadOnlyList<int>, bool> isMatch = code =>
            {
                return code.Any(c => _invalidDigits.Contains(c));
            };

            RemoveInvalidCode(isMatch);
        }

        private void RemoveInvalidCode(Func<IReadOnlyList<int>, bool> isInvalid)
        {
            for (var i = _allCodes.Count - 1; i >= 0; i--)
            {
                var code = _allCodes[i];

                if (isInvalid(code))
                {
                    _allCodes.RemoveAt(i);
                }
            }
        }

        private void AddInvalidDigits(IEnumerable<int> digits)
        {
            _invalidDigits.AddRange(digits.Except(_invalidDigits));
        }
    }
}