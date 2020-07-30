using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class DefaultPlaylistNameGenerator : IDefaultPlaylistNameGenerator
    {
        public string Generate(DateTime createdAt, SpotifyPlaylist playlist)
        {
            return "Shuffled " + playlist.Name;
        }
    }
}