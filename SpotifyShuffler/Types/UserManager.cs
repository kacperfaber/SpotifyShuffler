using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Types
{
    public class UserManager : UserManager<User>
    {
        public SpotifyContext SpotifyContext;

        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger, SpotifyContext spotifyContext) : base(store,
            optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            SpotifyContext = spotifyContext;
        }

        public override async Task<User> GetUserAsync(ClaimsPrincipal principal)
        {
            string id = principal?.Claims?.FirstOrDefault(x => x?.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return null;

            return await SpotifyContext.Users
                .Include(x => x.CompletedPlaylists)
                .ThenInclude(x => x.PlaylistPrototype)
                .ThenInclude(x => x.Tracks)
                .Include(x => x.SpotifyAccount)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        }
    }
}