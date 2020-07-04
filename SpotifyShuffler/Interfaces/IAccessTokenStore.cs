using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler.Interfaces
{
    public interface IAccessTokenStore
    {
        void StoreAccessToken(User user, IEnumerable<AuthenticationToken> tokens);
    }
}