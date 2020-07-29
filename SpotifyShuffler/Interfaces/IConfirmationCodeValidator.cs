using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IConfirmationCodeValidator
    {
        Task<bool> ValidateAsync(ConfirmationCode confirmationCode);
    }
}