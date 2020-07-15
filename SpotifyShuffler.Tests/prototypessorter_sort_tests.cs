using System.Collections.Generic;
using NUnit.Framework;
using SpotifyShuffler.Database;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Tests
{
    public class prototypessorter_sort_tests
    {

        void exec(ref List<TrackPrototype> prototypes)
        {
            new PrototypesSorter().Sort(ref prototypes);
        }

        [Test]
        public void dont_throws_exceptions()
        {
            List<TrackPrototype> prototypes = new List<TrackPrototype>();
            
            Assert.DoesNotThrow(() => exec(ref prototypes));
        }
    }
}