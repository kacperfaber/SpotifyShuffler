using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class DefaultOperationKindGenerator : IDefaultOperationKindGenerator
    {
        public OperationKind Generate()
        {
            return OperationKind.CreateNewPlaylist;
        }
    }
}