using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class SpotifyEmailIsSameChecker : ISpotifyEmailIsSameChecker
    {
        public bool Check(EmailAddress emailAddress, SpotifyAccount spotifyAccount)
        {
            return string.Equals(emailAddress.Email, spotifyAccount.EmailAddress, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}