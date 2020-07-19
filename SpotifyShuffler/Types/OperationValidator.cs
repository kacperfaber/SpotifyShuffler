using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class OperationValidator : IOperationValidator
    {
        public async Task<bool> ValidateAsync(Operation operation)
        {
            return await Task.Run(() => operation?.Prototype != null && operation.IsSubmitted && !string.IsNullOrEmpty(operation.PlaylistName));
        }
    }
}