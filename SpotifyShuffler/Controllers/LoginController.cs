using System;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyShuffler.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("regiter")]
        public IActionResult Register()
        {
            return Content("register account");
        }

        public IActionResult Login()
        {
            throw new NotImplementedException();
        }
    }
}