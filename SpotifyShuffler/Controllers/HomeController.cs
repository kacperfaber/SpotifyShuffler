using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<User> UserManager;
        public SpotifyContext Context;

        public HomeController(UserManager<User> userManager, SpotifyContext context)
        {
            UserManager = userManager;
            Context = context;
        }

        [HttpGet("home")]
        public async Task<IActionResult> Home()
        {
            return View(new HomeModel
            {
                CurrentUser = await UserManager.GetUserAsync(HttpContext.User)
            });
        }
    }
}