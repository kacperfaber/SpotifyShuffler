using System.Collections.Generic;
using NUnit.Framework;
using SpotifyShuffler.Database;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Tests
{
    public class prototypessorter_sort_tests
    {

        IEnumerable<TrackPrototype> exec(ref IEnumerable<TrackPrototype> prototypes)
        {
            // new PrototypesSorter().Sort(ref prototypes);

            return prototypes;
        }

        [Test]
        public void dont_throws_exceptions()
        {
            IEnumerable<TrackPrototype> prototypes = new List<TrackPrototype>()
            {
                new TrackPrototype()
                {
                    Author = "1"
                },
                new TrackPrototype()
                {
                    Author = "2"
                },
                new TrackPrototype()
                {
                    Author = "3"
                }
            };

            exec(ref prototypes);
        }
    }
}