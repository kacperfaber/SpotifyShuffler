using System.Linq;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ConfirmationCodeProvider : IConfirmationCodeProvider
    {
        public SpotifyContext SpotifyContext;

        public ConfirmationCodeProvider(SpotifyContext spotifyContext)
        {
            SpotifyContext = spotifyContext;
        }

        public ConfirmationCode Provide(string email, string code)
        {
            return SpotifyContext.ConfirmationCodes.Where(x => !x.IsUsed && !x.IsDeactivated)
                .FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Code == code);
        }
    }
}