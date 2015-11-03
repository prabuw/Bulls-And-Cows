using BullsAndCows.Human.Models;

namespace BullsAndCows.Human.Interfaces
{
    public interface IVerifier
    {
        VerificationResult Verify(string code, string guess);
    }
}