using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler.Interfaces
{
    public interface IUserCreator
    {
        Task<User> CreateUser(string email, string username, UserLoginInfo loginInfo);
    }
}