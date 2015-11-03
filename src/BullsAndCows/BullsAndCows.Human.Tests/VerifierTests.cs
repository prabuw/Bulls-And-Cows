using System.Collections;
using BullsAndCows.Human.Interfaces;
using NUnit.Framework;

namespace BullsAndCows.Human.Tests
{
    [TestFixture]
    public class VerifierTests
    {
        private readonly IVerifier _verifier;
        private const string ExpectedCode = "1234";

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