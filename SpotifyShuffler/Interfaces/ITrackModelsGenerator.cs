using System.Collections.Generic;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Interfaces
{
    public interface ITrackModelsGenerator
    {
        IEnumerable<TrackModel> Generate(IEnumerable<SpotifyTrack> tracks);
    }
}