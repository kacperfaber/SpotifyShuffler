using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database.Models;

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

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
        {
            AuthenticationProperties properties = new AuthenticationProperties
            {
                RedirectUri = "/callback"
            };
            
            return new ChallengeResult("Spotify", properties);
        }

        [AllowAnonymous]
        [HttpGet("callback")]
        public IActionResult ExternalCallback()
        {
            return Content("callback");
        }
    }
}