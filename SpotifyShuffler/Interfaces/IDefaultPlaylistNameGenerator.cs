using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface IDefaultPlaylistNameGenerator
    {
        string Generate(DateTime createdAt, SpotifyPlaylist playlist);
    }
}