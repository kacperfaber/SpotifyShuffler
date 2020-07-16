namespace SpotifyShuffler.Interface
{
    public class Authorization : ISpotifyAuthorization
    {
        public string AccessToken { get; set; }
        public string AccessTokenType { get; set; } = "Bearer";
        

        public string GetToken() => $"{AccessTokenType} {AccessToken}";
    }
}