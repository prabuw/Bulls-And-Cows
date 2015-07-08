
namespace BullsAndCows.Core
{
    public interface IVerifier
    {
        VerificationResult Verify(string code, string guess);
    }
}
