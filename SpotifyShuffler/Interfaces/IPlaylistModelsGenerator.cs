using System.Collections.Generic;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Interfaces
{
    public interface IPlaylistModelsGenerator
    {
        IEnumerable<PlaylistModel> Generate(IEnumerable<SimpleSpotifyPlaylist> playlists);
    }
}