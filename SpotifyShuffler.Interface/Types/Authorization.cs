namespace SpotifyShuffler.Interface
{
    public class Authorization : ISpotifyAuthorization
    {
        public string AccessToken { get; set; }
        public string AccessTokenType { get; set; }
        

        public string GetToken() => $"{AccessTokenType} {AccessToken}";
    }
}