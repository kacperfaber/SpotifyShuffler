using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class UserGenerator : IUserGenerator
    {
        public User GenerateUser(string username)
        {
            // TODO Add SpotifyAccount.
            
            return new User
            {
                UserName = username,
                Id = Guid.NewGuid()
            };
        }
    }
}