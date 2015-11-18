namespace BullsAndCows.Computer.Models
{
    public struct DigitWithPosition
    {
        public readonly int Digit;
        public readonly int Position;

        public DigitWithPosition(int digit, int position)
        {
            Digit = digit;
            Position = position;
        }
    }
}