using Microsoft.AspNetCore.Identity;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class UserLoginInfoGenerator : IUserLoginInfoGenerator
    {
        public UserLoginInfo GenerateLoginInfo(string spotifyId)
        {
            UserLoginInfo res = new UserLoginInfo("Spotify", spotifyId, "Spotify");

            return res;
        }
    }
}