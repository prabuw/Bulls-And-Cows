using System.Linq;
using BullsAndCows.Core;
using BullsAndCows.Core.Interfaces;
using NUnit.Framework;

namespace BullsAndCows.Tests
{
    [TestFixture]
    public class RandomCodeGeneratorTests
    {
        private readonly IRandomCodeGenerator _codeGenerator;

        public RandomCodeGeneratorTests()
        {
            _codeGenerator = new RandomCodeGenerator();
        }

        [Test]
        public void CodeMustHaveLengthOfFour()
        {
            var code = _codeGenerator.Generate();

            Assert.AreEqual(4, code.Length);
        }

        [Test]
        public void CodeMustNotContainTheNumberZero()
        {
            var code = _codeGenerator.Generate();

            var isZeroDigitInCode = code.Any(c => c == 0);

            Assert.False(isZeroDigitInCode);
        }

        [Test]
        public void CodeMustNotRepeatDigits()
        {
            var code = _codeGenerator.Generate();

            var isThereRepetitions = code.GroupBy(c => c).Any(g => g.Count() > 1);

            Assert.False(isThereRepetitions);
        }
    }
}
