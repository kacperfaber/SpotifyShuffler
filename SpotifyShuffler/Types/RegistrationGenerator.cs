using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class RegistrationGenerator : IRegistrationGenerator
    {
        public Registration GenerateRegistration(SpotifyAccount account)
        {
            return new Registration
            {
                Id = Guid.NewGuid(),
                SpotifyAccount = account,
                CreatedAt = DateTime.Now
            };
        }
    }
}