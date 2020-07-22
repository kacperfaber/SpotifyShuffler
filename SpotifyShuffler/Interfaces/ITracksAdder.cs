using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ITracksAdder
    {
        Task AddAll(PlaylistPrototype prototype, SpotifyPlaylist playlist, PlaylistService playlistService);
    }
}