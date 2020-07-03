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
            DbContextOptions options = new DbContextOptionsBuilder()
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
        public void dont_throws_exception_when_adding_new_PlaylistPrototypeData()
        {
            SpotifyContext ctx = exec();
            
            Assert.DoesNotThrow(() =>
            {
                ctx.PlaylistPrototypeDatas.Add(new PlaylistPrototypeData("title", "desc"));
                ctx.SaveChanges();
            });
        }
    }
}