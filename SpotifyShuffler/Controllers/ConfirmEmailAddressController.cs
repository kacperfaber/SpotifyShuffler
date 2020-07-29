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

        public async Task<IActionResult> Confirm()
        {
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