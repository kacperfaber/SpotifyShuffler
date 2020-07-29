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

        public CreateEmailAddressController(UserManager userManager)
        {
            UserManager = userManager;
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

            if (ModelState.IsValid)
            {
                if (model.IsCodeSent)
                {
                    // validate code.
                }

                else
                {
                    model.IsCodeSent = true;

                    // create email.
                    // create and send ConfirmationCode not related with EmailAddress created above - { code, email, id }.
                }
            }

            return await CreateEmail(model);
        }
    }
}