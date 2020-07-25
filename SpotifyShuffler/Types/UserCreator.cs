using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class UserCreator : IUserCreator
    {
        public IUserLoginInfoGenerator LoginInfoGenerator;
        public IRegistrationValidator RegistrationValidator;
        public SignInManager<User> SignInManager;
        public IUserGenerator UserGenerator;
        public UserManager<User> UserManager;

        public UserCreator(UserManager<User> userManager, IUserGenerator userGenerator, IRegistrationValidator registrationValidator,
            IUserLoginInfoGenerator loginInfoGenerator)
        {
            UserManager = userManager;
            UserGenerator = userGenerator;
            RegistrationValidator = registrationValidator;
            LoginInfoGenerator = loginInfoGenerator;
        }

        public async Task<User> CreateUserAsync(Registration registration)
        {
            if (RegistrationValidator.Validate(registration))
            {
                User user = UserGenerator.GenerateUser(registration.UserName, registration.EmailAddress);
                IdentityResult result = await UserManager.CreateAsync(user);

                if (result.Succeeded) await UserManager.AddLoginAsync(user, LoginInfoGenerator.GenerateLoginInfo(user.SpotifyAccount.SpotifyId));

                return user;
            }

            throw new InvalidOperationException($"Registration {registration.Id} is not validated successfully.");
        }
    }
}