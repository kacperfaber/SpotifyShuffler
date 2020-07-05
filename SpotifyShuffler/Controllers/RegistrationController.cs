using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    [Route("registration")]
    public class RegistrationController : Controller
    {
        public SpotifyContext SpotifyContext;
        public IUserCreator UserCreator;
        public SignInManager<User> SignInManager;
        public IRegistrationActivator RegistrationActivator;

        public RegistrationController(SpotifyContext spotifyContext)
        {
            SpotifyContext = spotifyContext;
        }

        [HttpGet("complete-user-data")]
        public IActionResult CompleteUserData([FromQuery(Name = "registration_id")] Guid registrationId, [FromQuery(Name = "spotify_id")] string spotifyId)
        {
            Registration registration = SpotifyContext.Registrations
                .Include(x => x.SpotifyAccount)
                .FirstOrDefault(x => x.Id == registrationId && x.SpotifyAccount.SpotifyId == spotifyId);

            return View("CompleteUserData", new CompleteUserDataModel
            {
                SpotifyAccount = registration.SpotifyAccount,
                RegistrationId = registration.Id,
                SpotifyId = registration.SpotifyAccount.SpotifyId
            });
        }

        [HttpPost]
        public IActionResult TakeCompletedUserData(CompleteUserDataModel model)
        {
            Registration registration = SpotifyContext.Registrations
                .Include(x => x.SpotifyAccount)
                .FirstOrDefault(x => x.Id == model.RegistrationId && x.SpotifyAccount.SpotifyId == model.SpotifyId);

            registration.UserName = model.UserName;
            registration.EmailAddress = model.EmailAddress;

            SpotifyContext.SaveChanges();

            return RedirectToAction("ValidateEmail", new { });
        }

        public async Task<IActionResult> ValidateEmail([FromQuery(Name = "registration_id")] Guid registrationId, [FromQuery(Name = "spotify_id")] string spotifyId)
        {
            Registration registration = SpotifyContext.Registrations
                .Include(x => x.SpotifyAccount)
                .FirstOrDefault(x => x.Id == registrationId && x.SpotifyAccount.SpotifyId == spotifyId);

            if (registration.EmailAddress == registration.SpotifyAccount.EmailAddress)
            {
                RegistrationActivator.Activate(registration);

                User user = await UserCreator.CreateUserAsync(registration);
                
                await SignInManager.SignInAsync(user, false);
                
                return View("EmailValidatedThroughSpotify");
            }

            else
            {
                return View("ActivationCodeIsSended");
            }
        }

        [HttpGet("activate-email")]
        public async Task<IActionResult> ActivateEmail(
            [FromQuery(Name = "registration_id")] Guid registrationId, 
            [FromQuery(Name = "spotify_id")] string spotifyId,
            [FromQuery(Name = "code")] string code)
        {
            Registration registration = SpotifyContext.Registrations
                .Include(x => x.SpotifyAccount)
                .Where(x => x.ActivationCode != null)
                .FirstOrDefault(x => x.Id == registrationId && x.SpotifyAccount.SpotifyId == spotifyId && x.ActivationCode == code);
            
            RegistrationActivator.Activate(registration);
            
            User user = await UserCreator.CreateUserAsync(registration);
                
            await SignInManager.SignInAsync(user, false);

            return View("SuccessfullyActivated");
        }
    }
}