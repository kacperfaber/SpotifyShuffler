using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class SpotifyAccountGenerator : ISpotifyAccountGenerator
    {
        public Task<SpotifyAccount> GenerateAccount(ClaimsPrincipal principal)
        {
            string name = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            string id = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            string email = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            string ctry = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country).Value;
            string product = principal.Claims.FirstOrDefault(x => x.Type == "urn:spotify:product").Value;

            return Task.Run(() => new SpotifyAccount
            {
                Country = ctry,
                Name = name,
                SpotifyId = id,
                EmailAddress = email,
                AccountType = product == "free" || product == "open" ? SpotifyAccountType.Free : SpotifyAccountType.Premium
            });
        }
    }
}