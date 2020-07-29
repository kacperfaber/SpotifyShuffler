using System.Collections.Generic;
using System.Linq;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailAddressProvider : IEmailAddressProvider
    {
        public SpotifyContext SpotifyContext;

        public EmailAddressProvider(SpotifyContext spotifyContext)
        {
            SpotifyContext = spotifyContext;
        }

        public EmailAddress Provide(User user)
        {
            return user.EmailAddresses
                .Where(x => !x.IsDeleted && !x.IsDeactivated)
                .FirstOrDefault();
        }

        public EmailAddress Provide(string email)
        {
            return SpotifyContext.EmailAddresses
                .Where(x => !x.IsDeleted && !x.IsDeactivated)
                .FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public EmailAddress Provide(IEnumerable<EmailAddress> emailAddresses)
        {
            return emailAddresses
                .Where(x => !x.IsDeleted && !x.IsDeactivated)
                .FirstOrDefault();
        }
    }
}