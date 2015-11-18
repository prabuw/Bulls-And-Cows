using BullsAndCows.Computer.Interfaces;
using NUnit.Framework;

namespace BullsAndCows.Computer.Tests
{
    [TestFixture]
    public class GuessGeneratorTests
    {
        private readonly IGuessGenerator _guessGenerator;

        public GuessGeneratorTests()
        {
            _guessGenerator = new GuessGenerator();
        }
    }
}
