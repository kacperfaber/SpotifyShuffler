using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Models;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize]
    public class AccountSettingsController : Controller
    {
        public UserManager UserManager;
        public SpotifyContext SpotifyContext;
        public EmailAddressManager EmailAddressManager;

        public AccountSettingsController(UserManager userManager, SpotifyContext spotifyContext, EmailAddressManager emailAddressManager)
        {
            UserManager = userManager;
            SpotifyContext = spotifyContext;
            EmailAddressManager = emailAddressManager;
        }

        [HttpGet("account-settings/settings")]
        public async Task<IActionResult> Settings()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            return View("Settings", new SettingsModel
            {
                CurrentUser = user,
                EmailAddress = await EmailAddressManager.GetAsync(user),
                SpotifyAccount = SpotifyContext.SpotifyAccounts.FirstOrDefault(x => x.SpotifyId == user.SpotifyAccountId)
            });
        }
    }
}