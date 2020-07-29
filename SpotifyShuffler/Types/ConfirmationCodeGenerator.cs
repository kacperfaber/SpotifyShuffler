using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ConfirmationCodeGenerator : IConfirmationCodeGenerator

    {
        public ConfirmationCode Generate(string email)
        {
            return new ConfirmationCode
            {
                Id = Guid.NewGuid(),
                Code = new Random().Next(0, 1000000).ToString(),
                Email = email,
                CreatedAt = DateTime.Now
            };
        }
    }
}