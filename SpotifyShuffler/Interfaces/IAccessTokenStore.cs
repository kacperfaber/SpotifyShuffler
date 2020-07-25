using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IAccessTokenStore
    {
        void StoreAccessToken(User user, IEnumerable<AuthenticationToken> tokens);

        Task<string> GetAccessToken(User user);
    }
}