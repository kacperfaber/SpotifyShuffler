using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class TracksAdder : ITracksAdder
    {
        public ISpotifyUrisGenerator UrisGenerator;

        public TracksAdder(ISpotifyUrisGenerator urisGenerator)
        {
            UrisGenerator = urisGenerator;
        }

        public async Task AddAll(IOrderedEnumerable<SpotifyTrack> shuffledTracks, SpotifyPlaylist playlist, PlaylistService playlistService)
        {
            IEnumerable<string> uris = UrisGenerator.Generate(shuffledTracks);
            await playlistService.AddAllTracks(playlist.Id, uris);
        }
    }
}