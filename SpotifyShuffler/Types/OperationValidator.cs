using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class OperationValidator : IOperationValidator
    {
        public async Task<OperationValidationResult> ValidateAsync(Operation operation)
        {
            return await Task.Run(() =>
            {
                if (!operation.IsSubmitted)
                {
                    return OperationValidationResult.NotSubmitted;
                }

                else if (operation.IsCanceled)
                {
                    return OperationValidationResult.IsCanceled;
                }

                else if (operation.Kind == OperationKind.CreateNewPlaylist && string.IsNullOrEmpty(operation.PlaylistName))
                {
                    return OperationValidationResult.NoName;
                }

                return OperationValidationResult.Ok;
            });
        }
    }
}