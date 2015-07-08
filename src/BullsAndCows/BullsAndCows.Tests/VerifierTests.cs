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

        public VerifierTests()
        {
            _verifier = new Verifier();
        }

        public IEnumerable<string> InvalidFormatGuesses
        {
            get
            {
                return new[] 
                {
                    "123A",
                    "123$",
                    "12 3",
                    "ABC$",
                    "!@#$",
                    "    "
                };
            }
        }

        [TestCaseSource("InvalidFormatGuesses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Verify_ThrowsAnException_IfGuessUsesInvalidCharacters(string guess)
        {
            var expectedCode = "1234";

            _verifier.Verify(expectedCode, guess);
        }

        public IEnumerable<string> InvalidLengthGuesses
        {
            get
            {
                return new[] 
                {
                    "12345",
                    "23",
                    ""
                };
            }
        }

        [TestCaseSource("InvalidLengthGuesses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Verify_ThrowsAnException_IfGuessUsesMoreThanFourValidCharacters(string guess)
        {
            var expectedCode = "1234";

            _verifier.Verify(expectedCode, guess);
        }
    }
}
