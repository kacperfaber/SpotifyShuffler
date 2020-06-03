using System.Collections.Generic;
using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Database.Interfaces
{
    public interface ITracksGenerator
    {
        IEnumerable<Track> GenerateTracks(object n = null);
    }
}