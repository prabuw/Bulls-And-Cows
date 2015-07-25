using System;
using System.Collections.Generic;
using BullsAndCows.Core;
using BullsAndCows.Core.Interfaces;
using NUnit.Framework;

namespace BullsAndCows.Tests
{
    [TestFixture]
    public class GuessValidatorTests
    {
        private readonly IGuessValidator _validator;

        public GuessValidatorTests()
        {
            _validator = new GuessValidator();
        }

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

        [TestCaseSource("InvalidFormatGuesses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Validate_ThrowsAnException_IfValueUsesInvalidCharacters(string value)
        {
           _validator.Validate(value);
        }

        [TestCaseSource("InvalidLengthGuesses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Validate_ThrowsAnException_IfValueUsesMoreThanFourValidCharacters(string value)
        {
            _validator.Validate(value);
        }

        [TestCaseSource("RepeatedValidCharacterGuesses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Validate_ThrowsAnException_IfValueContainsRepeatedNumbers(string value)
        {
            _validator.Validate(value);
        }

        [TestCaseSource("GuessesContainingZero")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Your guess is not in the valid format. It should be contain four unique digits, excluding zero.")]
        public void Validate_ThrowsAnException_IfValueContainsZero(string value)
        {
            _validator.Validate(value);
        }

        [Test]
        public void Validate_ValidValue_ReturnsTrue()
        {
            var isValid = _validator.Validate("2819");

            Assert.True(isValid);
        }
    }
}
