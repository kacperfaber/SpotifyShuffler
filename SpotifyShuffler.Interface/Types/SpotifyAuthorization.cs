namespace SpotifyShuffler.Interface
{
    public class SpotifyAuthorization : ISpotifyAuthorization
    {
        public string AccessToken { get; set; }

        public string AccessTokenType { get; set; } = "Bearer";

        public string GetToken()
        {
            return $"{AccessTokenType} {AccessToken}";
        }

        public static SpotifyAuthorization Create(string accessToken, string tokenType = "Bearer")
        {
            return new SpotifyAuthorization
            {
                AccessToken = accessToken,
                AccessTokenType = tokenType
            };
        }
    }
}