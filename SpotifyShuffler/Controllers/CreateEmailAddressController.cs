using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Models;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize]
    public class CreateEmailAddressController : Controller
    {
        public UserManager UserManager;
        public EmailAddressManager EmailAddressManager;

        public CreateEmailAddressController(UserManager userManager, EmailAddressManager emailAddressManager)
        {
            UserManager = userManager;
            EmailAddressManager = emailAddressManager;
        }

        public async Task<IActionResult> CreateEmail(CreateEmailAddressModel model)
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            model ??= new CreateEmailAddressModel();
            model.CurrentUser = user;

            return View("CreateEmail", model);
        }

        public async Task<IActionResult> CreateEmailPost(CreateEmailAddressModel model)
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            model.CurrentUser = user;

            if (ModelState.IsValid)
            {
                EmailAddressResult createResult = await EmailAddressManager.CreateEmail(user, model.Email);

                if (createResult == EmailAddressResult.Created)
                {
                    return RedirectToAction("ConfirmEmail", "ConfirmEmailAddress", new {model.Email});
                }
            }

            return await CreateEmail(model);
        }
    }
}