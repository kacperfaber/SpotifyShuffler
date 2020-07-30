using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Models;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize]
    public class ConfirmEmailAddressController : Controller
    {
        public UserManager UserManager;
        public EmailAddressManager EmailAddressManager;

        public ConfirmEmailAddressController(EmailAddressManager emailAddressManager, UserManager userManager)
        {
            EmailAddressManager = emailAddressManager;
            UserManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmLink(ConfirmLinkModel model)
        {
            EmailAddressResult confirmResult = await EmailAddressManager.Confirm(model.Email, model.Code);

            if (confirmResult == EmailAddressResult.Confirmed)
            {
                return View("Success");
            }

            else
            {
                return View("BadData");
            }
        }

        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel model)
        {
            // TODO there is need to be second verification page.

            User user = await UserManager.GetUserAsync(HttpContext.User);

            EmailAddress emailAddress = await EmailAddressManager.GetAsync(user);

            if (emailAddress != null)
            {
                if (emailAddress.Email.ToLower() == model.Email)
                {
                    return View("ConfirmEmail", model);
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmailPost(ConfirmEmailModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.GetUserAsync(HttpContext.User);
                EmailAddressResult confirmationResult = await EmailAddressManager.Confirm(model.Email, model.Code);

                if (confirmationResult == EmailAddressResult.Confirmed)
                {
                    return View("Success", new ConfirmationSuccessModel
                    {
                        CurrentUser = user,
                        Email = model.Email
                    });
                }

                else if (confirmationResult == EmailAddressResult.BadCode)
                {
                    ModelState.AddModelError("Code", "This is code incorrect, please check it again and try again.");
                    return View("ConfirmEmail", model);
                }

                else if (confirmationResult == EmailAddressResult.MissingEmail)
                {
                    return View("MissingEmail", new ConfirmationMissingEmailModel
                    {
                        CurrentUser = user,
                        Email = model.Email
                    });
                }
            }

            return View("ConfirmEmail", model);
        }

        public IActionResult SendConfirmationLink()
        {
        }
    }
}