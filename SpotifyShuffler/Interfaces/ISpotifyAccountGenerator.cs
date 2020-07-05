using System.Security.Claims;
using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface ISpotifyAccountGenerator
    {
        Task<SpotifyAccount> GenerateAccount(ClaimsPrincipal principal);
    }
}