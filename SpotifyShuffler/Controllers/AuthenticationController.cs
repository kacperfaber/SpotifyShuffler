using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    public class AuthenticationController : Controller
    {
        public SignInManager<User> SignInManager;
        public UserManager<User> UserManager;
        public SpotifyContext Context;
        public IUserFinder UserFinder;
        public IUserCreator UserCreator;
        public IAccessTokenStore AccessTokenStore;
        public ISpotifyAccountGenerator SpotifyAccountGenerator;
        public IRegistrationGenerator RegistrationGenerator;

        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager, SpotifyContext context, IUserFinder userFinder,
            IUserCreator userCreator, IAccessTokenStore accessTokenStore, ISpotifyAccountGenerator spotifyAccountGenerator, IRegistrationGenerator registrationGenerator)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            Context = context;
            UserFinder = userFinder;
            UserCreator = userCreator;
            AccessTokenStore = accessTokenStore;
            SpotifyAccountGenerator = spotifyAccountGenerator;
            RegistrationGenerator = registrationGenerator;
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
                
                Registration registration = RegistrationGenerator.GenerateRegistration(spotifyAccount);

                await Context.Registrations.AddAsync(registration);
                await Context.SaveChangesAsync();

                return RedirectToAction("CompleteUserData", "Registration", new {registration_id = registration.Id, spotify_id = spotifyAccount.SpotifyId});
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

        [HttpGet("current")]
        public async Task<IActionResult> Current()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            
            return Content(user.Email);
        }

        [HttpGet("check")]
        public IActionResult Check()
        {
            return Content(Convert.ToString(HttpContext.User.Identity.IsAuthenticated));
        }
    }
}