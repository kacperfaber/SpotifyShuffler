using AspNet.Security.OAuth.Spotify;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyShuffler.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet("callback")]
        public IActionResult SpotifyCallback()
        {
            return View("Callback");
        }

        [HttpGet("authenticate")]
        public IActionResult Authenticate() => Challenge(SpotifyAuthenticationDefaults.AuthenticationScheme);
    }
}