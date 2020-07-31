using System.Collections.Generic;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Types
{
    public class TrackModelsGenerator : ITrackModelsGenerator
    {
        public ITrackModelGenerator ModelGenerator;

        public TrackModelsGenerator(ITrackModelGenerator modelGenerator)
        {
            ModelGenerator = modelGenerator;
        }

        public IEnumerable<TrackModel> Generate(IEnumerable<SpotifyTrack> tracks)
        {
            foreach (SpotifyTrack track in tracks)
            {
                yield return ModelGenerator.Generate(track);
            }
        }
    }
}