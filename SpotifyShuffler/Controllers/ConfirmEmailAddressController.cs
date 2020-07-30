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
                return View("Success", new ConfirmationSuccessModel {CurrentUser = null, Email = model.Email});
            }

            else
            {
                return View("BadData", new ConfirmationBadDataModel {CurrentUser = null});
            }
        }

        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel model)
        {
            // TODO there is need to be second verification page.

            User user = await UserManager.GetUserAsync(HttpContext.User);
            model.CurrentUser = user;

            EmailAddress emailAddress = await EmailAddressManager.GetAsync(user);

            if (emailAddress != null)
            {
                if (emailAddress.Email.ToLower() == model.Email.ToLower())
                {
                    EmailAddressResult sendResult = await EmailAddressManager.SendConfirmationLink(user, model.Email);

                    if (sendResult == EmailAddressResult.CodeSent)
                    {
                        return View("ConfirmEmail", model);
                    }

                    else if (sendResult == EmailAddressResult.Confirmed)
                    {
                        return View("Success", new ConfirmationSuccessModel {CurrentUser = user, Email = model.Email});
                    }

                    else
                    {
                        return Content("Unexpected result.");
                    }
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

        public async Task<IActionResult> SendConfirmationLink(string email)
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            EmailAddressResult sendResult = await EmailAddressManager.SendConfirmationLink(user, email);

            if (sendResult == EmailAddressResult.CodeSent)
            {
                return View("CodeSent", new ConfirmationCodeSentModel {Email = email, CurrentUser = user});
            }

            else if (sendResult == EmailAddressResult.Confirmed)
            {
                return View("Success", new ConfirmationSuccessModel {CurrentUser = user, Email = email});
            }

            else
            {
                return Content("Unexpected result.");
            }
        }
    }
}