using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SpotifyShuffler.Database.Contexts;

namespace SpotifyShuffler.Database.Tests
{
    public class spotifycontext_constructor_tests
    {
        SpotifyContext exec()
        {
            DbContextOptions<SpotifyContext> options = new DbContextOptionsBuilder<SpotifyContext>()
                .UseInMemoryDatabase("testdb2")
                .Options;

            return new SpotifyContext(options);
        }

        [Test]
        public void returns_typeof_SpotifyContext()
        {
            Assert.IsTrue(exec() is SpotifyContext);
        }

        [Test]
        public void dont_throws_exceptions()
        {
            Assert.DoesNotThrow(delegate { exec(); });
        }

        [Test]
        public void dont_throws_exceptions_when_saving_no_changes()
        {
            Assert.DoesNotThrow(() => exec().SaveChanges());
        }
    }
}