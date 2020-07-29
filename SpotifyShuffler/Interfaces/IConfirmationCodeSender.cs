using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IConfirmationCodeSender
    {
        Task SendAsync(ConfirmationCode confirmationCode);
    }
}