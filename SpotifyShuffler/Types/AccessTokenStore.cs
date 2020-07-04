using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class AccessTokenStore : IAccessTokenStore
    {
        public UserManager<User> UserManager;
        public IClaimGenerator ClaimGenerator;

        public AccessTokenStore(UserManager<User> userManager, IClaimGenerator claimGenerator)
        {
            UserManager = userManager;
            ClaimGenerator = claimGenerator;
        }

        public async void StoreAccessToken(User user, IEnumerable<AuthenticationToken> tokens)
        {
            string accessToken = tokens.FirstOrDefault(x => x.Name == "access_token").Value;

            IList<Claim> currentClaims = await UserManager.GetClaimsAsync(user);
            IEnumerable<Claim> targetClaims = currentClaims.Where(x => x.Type == "AccessToken");
            Claim claim = ClaimGenerator.GenerateClaim("AccessToken", accessToken);

            foreach (Claim targetClaim in targetClaims)
            {
                await UserManager.RemoveClaimAsync(user, targetClaim);
            }

            await UserManager.AddClaimAsync(user, claim);
        }

        public async Task<string> GetAccessToken(User user)
        {
            IEnumerable<Claim> claims = await UserManager.GetClaimsAsync(user);
            return claims.SingleOrDefault(x => x.Type == "AccessToken").Value;
        }
    }
}