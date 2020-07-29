using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ConfirmationCodeSender : IConfirmationCodeSender
    {
        public Task SendAsync(ConfirmationCode confirmationCode)
        {
            return Task.Run(() => {});
        }
    }
}