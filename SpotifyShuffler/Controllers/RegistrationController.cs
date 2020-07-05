using System;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Spotify;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    [Route("registration")]
    public class RegistrationController : Controller
    {
        public UserManager<User> UserManager;

        public RegistrationController(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        [Authorize(AuthenticationSchemes = "Spotify")]
        [HttpGet("enter-your-email")]
        public async Task<IActionResult> EnterYourEmail()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            if (user.EmailAddress.Activation.IsActivated)
            {
                return View("EmailIsAlreadyActivated");
            }

            return View("EnterYourEmail", new EnterYourEmailModel());
        }

        [HttpPost]
        public IActionResult ValidateEmail(string email)
        {
            return Content(email);
        }
    }
}