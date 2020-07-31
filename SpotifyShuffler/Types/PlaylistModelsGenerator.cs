using System.Collections.Generic;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Types
{
    public class PlaylistModelsGenerator : IPlaylistModelsGenerator
    {
        public IPlaylistModelGenerator ModelGenerator;

        public PlaylistModelsGenerator(IPlaylistModelGenerator modelGenerator)
        {
            ModelGenerator = modelGenerator;
        }

        public IEnumerable<PlaylistModel> Generate(IEnumerable<SimpleSpotifyPlaylist> playlists)
        {
            foreach (SimpleSpotifyPlaylist playlist in playlists)
            {
                yield return ModelGenerator.Generate(playlist);
            }
        }
    }
}