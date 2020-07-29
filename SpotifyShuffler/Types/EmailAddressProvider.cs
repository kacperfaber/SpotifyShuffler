using System.Collections.Generic;
using System.Linq;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailAddressProvider : IEmailAddressProvider
    {
        public EmailAddress Provide(User user)
        {
            return user.EmailAddresses
                .Where(x => !x.IsDeleted && !x.IsDeactivated)
                .FirstOrDefault();
        }

        public EmailAddress Provide(IEnumerable<EmailAddress> emailAddresses)
        {
            return emailAddresses
                .Where(x => !x.IsDeleted && !x.IsDeactivated)
                .FirstOrDefault();
        }
    }
}