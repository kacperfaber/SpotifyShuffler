using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IEmailAddressConfirmator
    {
        Task ConfirmAsync(EmailAddress emailAddress, EmailConfirmationMethod confirmationMethod);
    }
}