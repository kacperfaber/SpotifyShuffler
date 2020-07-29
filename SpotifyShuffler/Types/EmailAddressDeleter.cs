using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailAddressDeleter : IEmailAddressDeleter
    {
        public SpotifyContext SpotifyContext;

        public EmailAddressDeleter(SpotifyContext spotifyContext)
        {
            SpotifyContext = spotifyContext;
        }

        public async Task DeleteAsync(EmailAddress emailAddress)
        {
            emailAddress.IsDeleted = true;
            emailAddress.DeletedAt = DateTime.Now;

            SpotifyContext.Update(emailAddress);
            await SpotifyContext.SaveChangesAsync();
        }
    }
}