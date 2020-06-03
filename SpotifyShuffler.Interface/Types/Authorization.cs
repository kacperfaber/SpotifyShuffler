using SpotifyShuffler.Interface.Interfaces;

namespace SpotifyShuffler.Interface.Types
{
    public class Authorization : ISpotifyAuthorization
    {
        public string AccessToken { get; set; }
        public string AccessTokenType { get; set; }
        

        public string GetToken() => $"{AccessTokenType} {AccessToken}";
    }
}