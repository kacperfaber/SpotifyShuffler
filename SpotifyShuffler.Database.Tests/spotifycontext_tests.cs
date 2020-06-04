using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SpotifyShuffler.Database.Contexts;
using SpotifyShuffler.Database.Models;
using Authorization = System.Net.Authorization;

namespace SpotifyShuffler.Database.Tests
{
    public class spotifycontext_tests
    {
        [Test]
        public void SpotifyContext_is_DbContext()
        {
            Assert.IsTrue(typeof(DbContext).IsAssignableFrom(typeof(SpotifyContext)));
        }

        [Test]
        public void SpotifyContext_has_no_empty_constructor()
        {
            Assert.IsEmpty(typeof(SpotifyContext).GetConstructors().Where(x => x.GetParameters().Length == 0));
        }

        [Test]
        public void SpotifyContext_has_constructor_with_DbContextOptions_or_DbContextOptions_of_SpotifyContext()
        {
            ConstructorInfo[] constructors = typeof(SpotifyContext).GetConstructors();
            ConstructorInfo ctor = constructors.SingleOrDefault(x => x.GetParameters().FirstOrDefault(x => x.ParameterType == typeof(DbContextOptions<SpotifyContext>) || x.ParameterType == typeof(DbContextOptions)) != null);
            
            Assert.IsNotNull(ctor);
        }

        [TestCase(typeof(Track))]
        [TestCase(typeof(PrimaryArtist))]
        [TestCase(typeof(User))]
        [TestCase(typeof(SpotifyUser))]
        [TestCase(typeof(PlaylistPrototype))]
        [TestCase(typeof(Playlist))]
        [TestCase(typeof(SpotifyShuffler.Database.Models.Authorization))]
        [TestCase(typeof(ApiRequest))]
        [TestCase(typeof(ApiResponse))]
        public void SpotifyContext_has_DbSet_for(Type dbsetOf)
        {
            Type dbSet = typeof(DbSet<>).MakeGenericType(dbsetOf);
            PropertyInfo property = typeof(SpotifyContext).GetProperties().SingleOrDefault(x => x.PropertyType == dbSet);
            
            Assert.NotNull(property);
        }
    }
}