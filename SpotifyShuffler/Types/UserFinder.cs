using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class UserFinder : IUserFinder
    {
        public SpotifyContext SpotifyContext;

        public UserFinder(SpotifyContext spotifyContext)
        {
            SpotifyContext = spotifyContext;
        }

        public User FindUserByEmailOrNull(string email)
        {
            return SpotifyContext.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public User FindUserByIdOrNull(Guid id)
        {
            return SpotifyContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User FindUserByNameOrNull(string username)
        {
            return SpotifyContext.Users.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
        }

        public User FindUserBySpotifyIdOrNull(string providerKey)
        {
            IdentityUserLogin<Guid> userLogin = SpotifyContext.UserLogins
                .Where(x => x.ProviderDisplayName == "Spotify")
                .FirstOrDefault(x => x.ProviderKey == providerKey);

            if (userLogin == null)
            {
                return null;
            }

            return SpotifyContext.Users.FirstOrDefault(x => x.Id == userLogin.UserId);
        }
    }
}