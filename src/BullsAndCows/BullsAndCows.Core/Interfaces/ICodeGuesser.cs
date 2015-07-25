using System.Collections.Generic;
using BullsAndCows.Core.Models;

namespace BullsAndCows.Core.Interfaces
{
    public interface ICodeGuesser
    {
        IReadOnlyList<int> AllowedNumbers { get; }

        string CreateGuess(GuessHistory guessHistory);

        void RemoveNumbersThatAreNeitherCowsOrBulls(RatifiedGuess ratifiedGuess);
    }
}