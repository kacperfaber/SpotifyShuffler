using Microsoft.AspNetCore.Identity;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler.Interfaces
{
    public interface IUserGenerator
    {
        User GenerateUser(string emailAddress, string username);
    }
}