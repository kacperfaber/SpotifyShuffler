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

        public async Task<IActionResult> Confirm()
        {
            // TODO there is need to be second verification page.
            
            User user = await UserManager.GetUserAsync(HttpContext.User);

            EmailAddress emailAddress = await EmailAddressManager.GetAsync(user);

            if (emailAddress == null)
            {
                return RedirectToAction("CreateEmail", "CreateEmailAddress");
            }

            else if (emailAddress.IsConfirmed)
            {
                return View("AlreadyConfirmed", new ConfirmEmailAddressModel {CurrentUser = user, EmailAddress = emailAddress});
            }
            
            else if (!emailAddress.IsConfirmed)
            {
                return RedirectToAction("CreateEmail", "CreateEmailAddress", new {emailAddress.Email});
            }
            
            return BadRequest();
        }
    }
}