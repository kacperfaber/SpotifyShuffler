using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize]
    public class DeleteEmailAddressController : Controller
    {
        public UserManager UserManager;
        public IEmailAddressDeleter Deleter;
        public IEmailAddressProvider EmailAddressProvider;

        public DeleteEmailAddressController(UserManager userManager, IEmailAddressDeleter deleter, IEmailAddressProvider emailAddressProvider)
        {
            UserManager = userManager;
            Deleter = deleter;
            EmailAddressProvider = emailAddressProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            return View("Delete", new DeleteEmailAddressModel {CurrentUser = user});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteEmailAddressModel model)
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            await Deleter.DeleteAsync(EmailAddressProvider.Provide(user));

            return View("Success", model);
        }
    }
}