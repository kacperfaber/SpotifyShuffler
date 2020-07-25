using System.Text.RegularExpressions;
using NUnit.Framework;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Tests
{
    public class artistlabelgenerator_generate_tests
    {
        private string exec(params SimpleSpotifyArtist[] artists)
        {
            ArtistLabelGenerator gen = new ArtistLabelGenerator();
            return gen.Generate(artists);
        }

        [Test]
        public void dont_throws_exceptions()
        {
            Assert.DoesNotThrow(() => exec());
        }

        [TestCase("Linkin Park", "CB")]
        [TestCase("Serj Tankian", "Daron Malakian")]
        [TestCase("Seether", "Amy Lee")]
        public void returns_matching_to_pattern(string artist1, string artist2)
        {
            string s = exec(new SimpleSpotifyArtist {Name = artist1}, new SimpleSpotifyArtist {Name = artist2});

            Assert.IsTrue(Regex.IsMatch(s, $"{artist1}, {artist2}"));
        }

        [TestCase("Linkin Park", "CB")]
        [TestCase("Serj Tankian", "Daron Malakian")]
        [TestCase("Seether", "Amy Lee")]
        public void returns_expected_string(string artist1, string artist2)
        {
            string s = exec(new SimpleSpotifyArtist {Name = artist1}, new SimpleSpotifyArtist {Name = artist2});

            Assert.AreEqual($"{artist1}, {artist2}", s);
        }
    }
}