using System.Security.Claims;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ClaimGenerator : IClaimGenerator
    {
        public Claim GenerateClaim(string name, string value)
        {
            return new Claim(name, value);
        }
    }
}