using System.Collections.Generic;
using BullsAndCows.Computer.Models;

namespace BullsAndCows.Computer.Interfaces
{
    public interface IGuessGenerator
    {
        IReadOnlyList<Guess> GuessHistory { get; }

        Guess Generate();
        void Process(Guess guess);
    }
}