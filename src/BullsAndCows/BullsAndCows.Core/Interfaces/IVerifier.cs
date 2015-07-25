
using BullsAndCows.Core.Models;

namespace BullsAndCows.Core.Interfaces
{
    public interface IVerifier
    {
        VerificationResult Verify(string code, string guess);
    }
}
