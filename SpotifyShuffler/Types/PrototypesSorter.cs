using System;
using System.Linq;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PrototypesSorter : IPrototypesSorter
    {
        public IModelIndexer ModelIndexer;
        public Random Random = new Random();

        public PrototypesSorter(IModelIndexer modelIndexer)
        {
            ModelIndexer = modelIndexer;
        }

        public void Sort(PlaylistPrototype prototype)
        {
            prototype.Tracks = prototype.Tracks
                .OrderBy(x => new Random().Next(0, 10000))
                .ForEach(x => x.PlaylistPrototype = prototype)
                .ToList();

            ModelIndexer.Index(prototype.Tracks, x => x.Index);
        }
    }
}