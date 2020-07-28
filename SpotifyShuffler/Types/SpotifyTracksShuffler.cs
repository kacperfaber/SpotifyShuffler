using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class SpotifyTracksShuffler : ISpotifyTracksShuffler
    {
        public async Task<IOrderedEnumerable<SpotifyTrack>> ShuffleAsync(List<SpotifyTrack> tracks)
        {
            return await Task.Run(() => tracks.OrderBy(x => Guid.NewGuid()));
        }
    }
}