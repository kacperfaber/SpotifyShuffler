using Microsoft.AspNetCore.Identity;

namespace SpotifyShuffler.Interfaces
{
    public interface IUserLoginInfoGenerator
    {
        UserLoginInfo GenerateLoginInfo(string spotifyId);
    }
}