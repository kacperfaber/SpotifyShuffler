using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class UserGenerator : IUserGenerator
    {
        public User GenerateUser(string emailAddress, string username)
        {
            // TODO Add SpotifyAccount.
            
            return new User
            {
                Email = emailAddress,
                UserName = username,
                Id = Guid.NewGuid()
            };
        }
    }
}