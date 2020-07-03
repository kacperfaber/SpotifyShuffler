using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    public class AuthenticationController : Controller
    {
        public SignInManager<User> SignInManager;
        public UserManager<User> UserManager;

        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
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

            IdentityResult identityResult = await UserManager.CreateAsync(user);
            await UserManager.AddLoginAsync(user, userLoginInfo);
            await SignInManager.SignInAsync(user, properties);

            return View("SuccessfullyLoggedIn", new SuccessfullyLoggedInModel {User = user});
        }
    }
}