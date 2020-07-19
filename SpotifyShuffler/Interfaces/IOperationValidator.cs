using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IOperationValidator
    {
        Task<bool> ValidateAsync(Operation operation);
    }
}