using System;
using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class UserGenerator : IUserGenerator
    {
        public User GenerateUser(string emailAddress, string username)
        {
            return new User
            {
                Email = emailAddress,
                UserName = username,
                Id = Guid.NewGuid(),
                EmailAddress = new EmailAddress()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    Activation = new EmailAddressActivation()
                    {
                        Id = Guid.NewGuid(),
                        IsActivated = false
                    }
                }
            };
        }
    }
}