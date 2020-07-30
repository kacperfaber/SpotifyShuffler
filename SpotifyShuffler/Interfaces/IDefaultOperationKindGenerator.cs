using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IDefaultOperationKindGenerator
    {
        OperationKind Generate();
    }
}