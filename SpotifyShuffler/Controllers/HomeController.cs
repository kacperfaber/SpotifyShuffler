using Microsoft.AspNetCore.Mvc;

namespace SpotifyShuffler.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home(string name)
        {
            return View(name as object);
        }
    }
}