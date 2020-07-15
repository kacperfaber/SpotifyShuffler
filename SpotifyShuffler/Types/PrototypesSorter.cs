using System;
using System.Collections.Generic;
using System.Linq;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PrototypesSorter : IPrototypesSorter
    {
        public Random Random = new Random();

        public void Sort(ref List<TrackPrototype> prototypes)
        {
            int count = prototypes.Count;

            prototypes = prototypes.OrderBy(x => Random.Next(count)).ToList();
        }
    }
}