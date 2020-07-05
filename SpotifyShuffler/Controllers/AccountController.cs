using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyShuffler.Controllers
{
    public class AccountController : Controller
    {
        [Authorize(AuthenticationSchemes = "Spotify")]
        public IActionResult Settings()
        {
            throw new NotImplementedException();
        }
    }
}