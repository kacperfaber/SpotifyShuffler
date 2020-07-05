using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class UserGenerator : IUserGenerator
    {
        public User GenerateUser(string username, string emailAddress)
        {
            return new User
            {
                UserName = username,
                Email = emailAddress,
                EmailConfirmed = true,
                Id = Guid.NewGuid()
            };
        }
    }
}