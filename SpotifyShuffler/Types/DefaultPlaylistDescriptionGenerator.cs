using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class DefaultPlaylistDescriptionGenerator : IDefaultPlaylistDescriptionGenerator
    {
        public string Generate(DateTime createdAt, SpotifyPlaylist playlist)
        {
            return $"Playlist \"{playlist.Name}\" shuffled at {createdAt:g}. {playlist.Description}";
        }
    }
}