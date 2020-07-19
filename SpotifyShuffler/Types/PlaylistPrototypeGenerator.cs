using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistPrototypeGenerator : IPlaylistPrototypeGenerator
    {
        public ITrackPrototypesGenerator PrototypesGenerator;
        public ITrackPrototypesGenerator TrackPrototypesGenerator;
        public IModelIndexer ModelIndexer;

        public PlaylistPrototypeGenerator(ITrackPrototypesGenerator trackPrototypesGenerator, IPrototypesSorter sorter, IModelIndexer modelIndexer)
        {
            TrackPrototypesGenerator = trackPrototypesGenerator;
            ModelIndexer = modelIndexer;
        }

        public async Task<PlaylistPrototype> GenerateAsync(SpotifyPlaylist playlist, Operation operation)
        {
            List<TrackPrototype> tracks = (await TrackPrototypesGenerator.GenerateAsync(playlist))
                .OrderBy(x => new Random().Next(0, 10000))
                .ToList();
            
            ModelIndexer.Index(tracks, x => x.Index);
            
            PlaylistPrototype proto = new PlaylistPrototype
            {
                Id = Guid.NewGuid(),
                Tracks = tracks
            };
            
            return proto;
        }
    }
}