using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Controllers
{
    public class AuthenticationController : Controller
    {
        public IAccessTokenStore AccessTokenStore;
        public SignInManager<User> SignInManager;
        public ISpotifyAccountGenerator SpotifyAccountGenerator;
        public IUserFinder UserFinder;
        public UserManager<User> UserManager;

        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager, IUserFinder userFinder,
            IAccessTokenStore accessTokenStore, ISpotifyAccountGenerator spotifyAccountGenerator)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            UserFinder = userFinder;
            AccessTokenStore = accessTokenStore;
            SpotifyAccountGenerator = spotifyAccountGenerator;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            AuthenticationProperties properties = SignInManager.ConfigureExternalAuthenticationProperties("Spotify", "/callback");

            return new ChallengeResult("Spotify", properties);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> ExternalCallback()
        {
            ExternalLoginInfo loginInfo = await SignInManager.GetExternalLoginInfoAsync();

            User user = UserFinder.FindUserBySpotifyIdOrNull(loginInfo.ProviderKey);

            if (user == null)
            {
                SpotifyAccount spotifyAccount = await SpotifyAccountGenerator.GenerateAccount(loginInfo.Principal);

                User createdUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = spotifyAccount.EmailAddress,
                    UserName = spotifyAccount.Name,
                    SpotifyAccount = spotifyAccount,
                    EmailAddresses = new List<EmailAddress>()
                };

                _ = await UserManager.CreateAsync(createdUser);

                _ = await UserManager.AddLoginAsync(createdUser, loginInfo);

                AccessTokenStore.StoreAccessToken(createdUser, loginInfo.AuthenticationTokens);

                await SignInManager.SignInAsync(createdUser, true);

                return RedirectToAction("Home", "Home");
            }

            else
            {
                AccessTokenStore.StoreAccessToken(user, loginInfo.AuthenticationTokens);

                await SignInManager.SignInAsync(user, true);

                return RedirectToAction("Home", "Home");
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();

            return RedirectToAction("Home", "Home");
        }
    }
}