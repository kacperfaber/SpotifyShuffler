using Microsoft.AspNetCore.Mvc;

namespace SpotifyShuffler.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return RedirectToAction("Login", "Authentication");
        }
    }
}