using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IUserCreator
    {
        Task<User> CreateUser(string username, UserLoginInfo loginInfo);
    }
}