using System.Linq;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ITracksAdder
    {
        Task AddAll(IOrderedEnumerable<SpotifyTrack> shuffledTracks, SpotifyPlaylist playlist, PlaylistService playlistService);
    }
}