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
        public EmailAddressManager EmailAddressManager;

        public DeleteEmailAddressController(UserManager userManager, EmailAddressManager emailAddressManager)
        {
            UserManager = userManager;
            EmailAddressManager = emailAddressManager;
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            EmailAddress emailAddress = await EmailAddressManager.GetAsync(user);
            DeleteEmailAddressModel model = new DeleteEmailAddressModel {CurrentUser = user, EmailAddress = emailAddress};

            if (emailAddress == null)
            {
                return View("UserHasNoEmail", model);
            }

            return View("Delete", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteEmailAddressModel model)
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            await EmailAddressManager.DeleteAsync(owner: user);

            return View("Success", model);
        }
    }
}