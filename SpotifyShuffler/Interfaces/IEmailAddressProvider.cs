using System.Collections.Generic;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IEmailAddressProvider
    {
        EmailAddress Provide(User user);
        
        EmailAddress Provide(string email);
        EmailAddress Provide(IEnumerable<EmailAddress> emailAddresses);
    }
}