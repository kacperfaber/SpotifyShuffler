using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IEmailAddressDeleter
    {
        Task DeleteAsync(EmailAddress emailAddress);
    }
}