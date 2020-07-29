using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize]
    public class CreateEmailAddressController : Controller
    {
        public UserManager UserManager;
        
        public async Task<IActionResult> CreateEmail()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            return View();
        }
    }
}