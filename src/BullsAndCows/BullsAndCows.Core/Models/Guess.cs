
namespace BullsAndCows.Core.Models
{
    public class Guess
    {
        public readonly string Value;

        public Guess(string value)
        {
            ValidateGuess(value);
            Value = value;
        }

        private void ValidateGuess(string rawGuess)
        {
            var validator = new GuessValidator();
            validator.Validate(rawGuess);
        }
    }
}
