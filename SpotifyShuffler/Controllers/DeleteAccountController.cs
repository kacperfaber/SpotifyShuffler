using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize]
    public class DeleteAccountController : Controller
    {
        public SignInManager<User> SignInManager;
        public UserManager UserManager;
        public EmailAddressManager EmailAddressManager;
        public IEmailComparer EmailComparer;

        public DeleteAccountController(UserManager userManager, IEmailComparer emailComparer, EmailAddressManager emailAddressManager)
        {
            UserManager = userManager;
            EmailComparer = emailComparer;
            EmailAddressManager = emailAddressManager;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAccount()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            return View("DeleteAccount", new DeleteAccountModel {CurrentUser = user});
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccountPost(DeleteAccountModel model)
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            EmailAddress userEmail = await EmailAddressManager.GetAsync(user);

            if (EmailComparer.Compare(userEmail.Email, model.CurrentEmailAddress))
            {
                await UserManager.RemoveLoginAsync(user, "Spotify", user.SpotifyAccountId);
                await UserManager.DeleteAsync(user);

                return View("Success", model);
            }

            ModelState.AddModelError("CurrentEmailAddress", "Could not validate this email.");
            return View("DeleteAccount", model);
        }
    }
}