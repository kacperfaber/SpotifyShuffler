using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("home")]
        public IActionResult Home(string title) => View(new HomeModel() {Title = title});
    }
}