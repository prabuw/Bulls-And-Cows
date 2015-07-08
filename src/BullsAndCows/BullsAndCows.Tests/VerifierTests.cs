using System;
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
    }
}
