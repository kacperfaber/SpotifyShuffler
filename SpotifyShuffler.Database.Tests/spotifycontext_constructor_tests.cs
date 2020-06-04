using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SpotifyShuffler.Database.Contexts;
using SpotifyShuffler.Database.Models;

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

        [Test]
        public void dont_throws_exception_when_adding_new_track()
        {
            Track track = new Track
            {
                Id = Guid.NewGuid(),
                Name = "Somewhere i belong",
                GeneratedAt = DateTime.Now,
                PrimaryArtist = new PrimaryArtist()
                {
                    Id = Guid.NewGuid(),
                    Name = "Linkin Park",
                    SpotifyId = "XD"
                },
                DurationMilliseconds = 95825,
                SpotifyId = "spotify:xd"
            };

            SpotifyContext ctx = exec();
            
            Assert.DoesNotThrow(() =>
            {
                ctx.Tracks.Add(track);
                ctx.SaveChanges();
            });
        }
    }
}