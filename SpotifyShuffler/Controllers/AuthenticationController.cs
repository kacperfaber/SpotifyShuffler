using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database.Contexts;
using SpotifyShuffler.Database.Models;
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

        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager, SpotifyContext context, IUserFinder userFinder)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            Context = context;
            UserFinder = userFinder;
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

            AuthenticationProperties properties = new AuthenticationProperties();
            properties.StoreTokens(loginInfo.AuthenticationTokens);
            properties.IsPersistent = true;

            User user = UserFinder.FindUserBySpotifyIdOrNull(loginInfo.ProviderKey);
            
            if (user == null)
            {
                return View("ThatSpotifyUserIsNotRegistered", new LayoutModel());
            }

            else
            {
                await SignInManager.SignInAsync(user, false);
            }

            return View("SuccessfullyLoggedIn", new SuccessfullyLoggedInModel {User = user});
        }
    }
}