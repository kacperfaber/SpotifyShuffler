using System.Security.Claims;

namespace SpotifyShuffler.Interfaces
{
    public interface IClaimGenerator
    {
        Claim GenerateClaim(string name, string value);
    }
}