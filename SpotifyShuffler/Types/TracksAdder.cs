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
        public IOrderedPrototypesProvider OrderedPrototypesProvider;
        public ISpotifyUrisGenerator UrisGenerator;

        public TracksAdder(ISpotifyUrisGenerator urisGenerator, IOrderedPrototypesProvider orderedPrototypesProvider)
        {
            UrisGenerator = urisGenerator;
            OrderedPrototypesProvider = orderedPrototypesProvider;
        }

        public async Task AddAll(PlaylistPrototype prototype, SpotifyPlaylist playlist, PlaylistService playlistService)
        {
            IOrderedEnumerable<TrackPrototype> orderedTracks = OrderedPrototypesProvider.Provide(prototype);
            IEnumerable<string> uris = UrisGenerator.Generate(orderedTracks);
            await playlistService.AddAllTracks(playlist.Id, uris);
        }
    }
}