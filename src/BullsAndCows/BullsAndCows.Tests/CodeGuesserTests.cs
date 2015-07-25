using System.Linq;
using BullsAndCows.Core;
using BullsAndCows.Core.Interfaces;
using BullsAndCows.Core.Models;
using NSubstitute;
using NUnit.Framework;

namespace BullsAndCows.Tests
{
    [TestFixture]
    public class CodeGuesserTests
    {
        private readonly IRandomCodeGenerator _codeGenerator;
        private readonly ICodeGuesser _codeGuesser;

        public CodeGuesserTests()
        {
            _codeGenerator = Substitute.For<IRandomCodeGenerator>();
            _codeGuesser = new CodeGuesser(_codeGenerator);
        }

        [Test]
        public void CodeGuesserStartsWithAllowedNumbersFromOneToNine()
        {
            for (var i = 1; i < 10; i++)
            {
                CollectionAssert.Contains(_codeGuesser.AllowedNumbers, i);
            }
        }

        [Test]
        public void IfGuessHistoryIsEmpty_UseCodeGeneratorTo_GenerateFirstCodeRandomly()
        {
            var guessHistory = new GuessHistory();
            _codeGenerator.Generate().Returns(new[] { 2, 8, 1, 9 });

            var guess = _codeGuesser.CreateGuess(guessHistory);

            Assert.AreEqual("2819", guess);
        }

        [Test]
        public void CreatedGuessIsStoredAsUnratifiedGuess()
        {
            var guessHistory = new GuessHistory();

            _codeGenerator.Generate().Returns(new[] {1, 2, 3, 4});
            var guess = _codeGuesser.CreateGuess(guessHistory);

            var isGuessInHistory = guessHistory.UnratifiedGuess.ToString() == guess;

            Assert.True(isGuessInHistory);
        }

        [Test]
        public void DoNotCreateExistingGuesses()
        {
            var guessHistory = new GuessHistory();
            guessHistory.AddGuess(new[] { 2, 8, 1, 9 });

            var guess = _codeGuesser.CreateGuess(guessHistory);

            Assert.AreNotEqual("2813", guess);
        }

        [Test]
        public void RemoveNumbersThatAreNeitherCowsOrBulls()
        {
            var guess = new[] {1, 2, 3, 4};
            var ratifiedGuess = new RatifiedGuess(guess, 0, 0);
            
            _codeGuesser.RemoveNumbersThatAreNeitherCowsOrBulls(ratifiedGuess);

            foreach (var digit in guess)
            {
                CollectionAssert.DoesNotContain(_codeGuesser.AllowedNumbers, digit);
            }
        }
    }
}
