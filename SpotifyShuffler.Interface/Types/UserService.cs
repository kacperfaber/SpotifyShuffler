using System;

namespace SpotifyShuffler.Interface
{
    public class UserService
    {
        public ISpotifyAuthorization Authorization { get; set; }

        public PrivateSpotifyUser CurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}