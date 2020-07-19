using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IOperationValidator
    {
        Task<OperationValidationResult> ValidateAsync(Operation operation);
    }
}