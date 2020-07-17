using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;

namespace SpotifyShuffler.Database.Tests
{
    public class user_tests
    {
        [Test]
        public void User_is_IdentityUser_of_Guid()
        {
            Assert.IsTrue(typeof(IdentityUser<Guid>).IsAssignableFrom(typeof(User)));
        }

        [Test]
        public void has_empty_constructor()
        {
            Assert.IsNotEmpty(typeof(User).GetConstructors().Where(x => x.GetParameters().Length == 0));
        }

        [Test]
        public void has_list_of_Operation()
        {
            Assert.IsNotEmpty(typeof(User).GetProperties().Where(x => x.PropertyType == typeof(List<Operation>)));
        }
    }
}