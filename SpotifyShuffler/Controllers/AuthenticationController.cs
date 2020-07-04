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
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    public class AuthenticationController : Controller
    {
        public SignInManager<User> SignInManager;
        public UserManager<User> UserManager;
        public SpotifyContext Context;

        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager, SpotifyContext context)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            Context = context;
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

            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = loginInfo.Principal.FindFirst(ClaimTypes.Name).Value
            };

            UserLoginInfo userLoginInfo = new UserLoginInfo(loginInfo.LoginProvider, loginInfo.ProviderKey, loginInfo.ProviderDisplayName);

            await UserManager.CreateAsync(user);
            await UserManager.AddLoginAsync(user, userLoginInfo);

            User dbUser = Context.Users.FirstOrDefault();

            await SignInManager.SignInAsync(dbUser, properties);

            return View("SuccessfullyLoggedIn", new SuccessfullyLoggedInModel {User = user});
        }
    }
}