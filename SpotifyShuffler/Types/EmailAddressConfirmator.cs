using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailAddressConfirmator : IEmailAddressConfirmator
    {
        public SpotifyContext SpotifyContext;

        public EmailAddressConfirmator(SpotifyContext spotifyContext)
        {
            SpotifyContext = spotifyContext;
        }

        public async Task ConfirmAsync(EmailAddress emailAddress, EmailConfirmationMethod confirmationMethod)
        {
            emailAddress.ConfirmationMethod = confirmationMethod;
            emailAddress.ConfirmedAt = DateTime.Now;
            emailAddress.IsConfirmed = true;

            SpotifyContext.Update(emailAddress);
            await SpotifyContext.SaveChangesAsync();
        }
    }
}