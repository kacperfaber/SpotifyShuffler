using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

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
        public void SpotifyContext_has_empty_constructor()
        {
            Assert.IsNotEmpty(typeof(SpotifyContext).GetConstructors().Where(x => x.GetParameters().Length == 0));
        }

        [Test]
        public void has_parametrized_constructor()
        {
            Assert.IsNotEmpty(typeof(SpotifyContext).GetConstructors().Where(x => x.GetParameters().Any()));
        }

        [Test]
        public void has_constructor_with_single_DbContextOptions_parameter()
        {
            object ctor = typeof(SpotifyContext)
                .GetConstructors()
                .SingleOrDefault(x => x.GetParameters().FirstOrDefault()?.ParameterType == typeof(DbContextOptions));

            Assert.NotNull(ctor);
        }

        [TestCase(typeof(User))]
        public void SpotifyContext_has_DbSet_for(Type dbsetOf)
        {
            Type dbSet = typeof(DbSet<>).MakeGenericType(dbsetOf);
            PropertyInfo property = typeof(SpotifyContext).GetProperties().SingleOrDefault(x => x.PropertyType == dbSet);

            Assert.NotNull(property);
        }
    }
}