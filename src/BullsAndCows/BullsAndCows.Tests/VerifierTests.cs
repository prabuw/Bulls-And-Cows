using System;
using System.Collections;
using System.Collections.Generic;
using BullsAndCows.Core;
using NUnit.Framework;

namespace BullsAndCows.Tests
{
    [TestFixture]
    public class VerifierTests
    {
        private readonly IVerifier _verifier;

        private const string ExpectedCode = "1234";

        private static IEnumerable<string> InvalidFormatGuesses
        {
            get
            {
                yield return "123A";
                yield return "123$";
                yield return "12 3";
                yield return "ABC$";
                yield return "!@#$";
                yield return "    ";
            }
        }

        private static IEnumerable<string> InvalidLengthGuesses
        {
            get
            {
                yield return "12345";
                yield return "23";
                yield return "";
            }
        }

        private static IEnumerable<string> RepeatedValidCharacterGuesses
        {
            get
            {
                yield return "1111";
                yield return "1223";
                yield return "2234";
            }
        }

        private static IEnumerable<string> GuessesContainingZero
        {
            get
            {
                yield return "1230";
                yield return "0145";
                yield return "2034";
            }
        }

        private static IEnumerable ValidBullGuesses
        {
            get
            {
                yield return new TestCaseData("4321", 0);
                yield return new TestCaseData("1567", 1);
                yield return new TestCaseData("4231", 2);
                yield return new TestCaseData("1236", 3);
                yield return new TestCaseData("1234", 4);
            }
        }

        private static IEnumerable ValidCowGuesses
        {
            get
            {
                yield return new TestCaseData("8765", 0);
                yield return new TestCaseData("8561", 1);
                yield return new TestCaseData("4178", 2);
                yield return new TestCaseData("3127", 3);
                yield return new TestCaseData("4321", 4);
            }
        }


        public VerifierTests()
        {
            _verifier = new Verifier();
        }

        [TestCaseSource("InvalidFormatGuesses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Verify_ThrowsAnException_IfGuessUsesInvalidCharacters(string guess)
        {   
            _verifier.Verify(ExpectedCode, guess);
        }

        [TestCaseSource("InvalidLengthGuesses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Verify_ThrowsAnException_IfGuessUsesMoreThanFourValidCharacters(string guess)
        {
            _verifier.Verify(ExpectedCode, guess);
        }

        [TestCaseSource("RepeatedValidCharacterGuesses")]
        [ExpectedException(typeof (ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Verify_ThrowsAnException_IfGuessContainsRepeatedNumbers(string guess)
        {
            _verifier.Verify(ExpectedCode, guess);
        }

        [TestCaseSource("GuessesContainingZero")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Verify_ThrowsAnException_IfGuessContainsZero(string guess)
        {
            _verifier.Verify(ExpectedCode, guess);
        }

        [TestCaseSource("ValidBullGuesses")]
        public void Verify_ReturnsCorrectNumberOfBulls(string guess, int expectedBullCount)
        {
            var result = _verifier.Verify(ExpectedCode, guess);

            Assert.AreEqual(expectedBullCount, result.Bulls);
        }

        [TestCaseSource("ValidCowGuesses")]
        public void Verify_ReturnsCorrectNumberOfCows(string guess, int expectedCowCount)
        {
            var result = _verifier.Verify(ExpectedCode, guess);

            Assert.AreEqual(expectedCowCount, result.Cows);
        }
    }
}
