using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailAddressGenerator : IEmailAddressGenerator
    {
        public EmailAddress Generate(User user, string email)
        {
            return new EmailAddress
            {
                Id = Guid.NewGuid(),
                Email = email,
                User = user,
                CreatedAt = DateTime.Now
            };
        }
    }
}