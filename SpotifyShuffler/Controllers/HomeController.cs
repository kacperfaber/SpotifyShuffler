using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database.Contexts;
using SpotifyShuffler.Database.Models;
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
        public IActionResult Home(string title)
        {
            return View(new HomeModel
            {
                Title = title, 
                CurrentUser = UserManager.GetUserAsync(HttpContext.User).Result
            });
        }
    }
}