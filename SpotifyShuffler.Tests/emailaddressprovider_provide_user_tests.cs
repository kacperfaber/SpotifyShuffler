using System;
using System.Collections.Generic;
using NUnit.Framework;
using SpotifyShuffler.Database;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Tests
{
    public class emailaddressprovider_provide_user_tests
    {
        User user()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                EmailAddresses = new List<EmailAddress>
                {
                    new EmailAddress
                    {
                        Id = Guid.NewGuid(),
                        Email = "hello@world.com"
                    },
                    new EmailAddress
                    {
                        Id = Guid.NewGuid(),
                        Email = "hello",
                        IsDeactivated = true
                    },
                    new EmailAddress
                    {
                        Id = Guid.NewGuid(),
                        Email = "dkoapda",
                        IsDeleted = true
                    }
                }
            };
        }
        
        EmailAddress exec(User user)
        {
            return new EmailAddressProvider(null).Provide(user);
        }

        [Test]
        public void dont_throws_exceptions()
        {
            Assert.DoesNotThrow(() => exec(user()));
        }

        [Test]
        public void returns_EmailAddress_Email_equals_to_hello_world_com()
        {
            Assert.AreEqual("hello@world.com", exec(user()).Email);
        }

        [Test]
        public void dont_returns_EmailAddress_with_IsDeleted_is_true()
        {
            Assert.IsFalse(exec(user()).IsDeleted);
        }
        
        [Test]
        public void dont_returns_EmailAddress_with_IsDeactivated_is_true()
        {
            Assert.IsFalse(exec(user()).IsDeactivated);
        }
    }
}