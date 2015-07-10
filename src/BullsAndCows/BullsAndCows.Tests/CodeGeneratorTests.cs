using System;
using System.Linq;
using BullsAndCows.Core;
using NUnit.Framework;

namespace BullsAndCows.Tests
{
    [TestFixture]
    public class CodeGeneratorTests
    {
        private readonly ICodeGenerator _codeGenerator;

        public CodeGeneratorTests()
        {
            _codeGenerator = new CodeGenerator();
        }

        [Test]
        public void CodeMustHaveLengthOfFour()
        {
            var code = _codeGenerator.Generate();

            Assert.AreEqual(4, code.Length);
        }

        [Test]
        public void CodeMustContainNumericCharactersOnly()
        {
            var code = _codeGenerator.Generate();

            var isAllCharactersNumeric = code.All(Char.IsNumber);

            Assert.True(isAllCharactersNumeric);
        }

        [Test]
        public void CodeMustNotContainTheNumberZero()
        {
            var code = _codeGenerator.Generate();

            var isZeroDigitInCode = code.Any(c => c == '0');

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
