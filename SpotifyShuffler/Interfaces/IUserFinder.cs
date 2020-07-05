using System;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IUserFinder
    {
        User FindUserByEmailOrNull(string email);

        User FindUserByIdOrNull(Guid id);

        User FindUserByNameOrNull(string username);
        User FindUserBySpotifyIdOrNull(string providerKey);
    }
}