using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class UserCreator : IUserCreator
    {
        public UserManager<User> UserManager;
        public IUserGenerator UserGenerator;

        public UserCreator(UserManager<User> userManager, IUserGenerator userGenerator)
        {
            UserManager = userManager;
            UserGenerator = userGenerator;
        }

        public async Task<User> CreateUser(string email, string username, UserLoginInfo loginInfo)
        {
            User user = UserGenerator.GenerateUser(email, username);

            IdentityResult createResult = await UserManager.CreateAsync(user);
            
            if (createResult.Succeeded)
            {
                await UserManager.AddLoginAsync(user, loginInfo);
            }

            return user;
        }
    }
}