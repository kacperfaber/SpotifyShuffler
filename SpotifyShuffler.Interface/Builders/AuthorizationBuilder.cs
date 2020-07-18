namespace SpotifyShuffler.Interface
{
    public class AuthorizationBuilder : Builder<SpotifyAuthorization, AuthorizationBuilder>
    {
        public AuthorizationBuilder AccessToken(string accessToken, string accessTokenType = "Bearer")
        {
            return Update(x =>
            {
                x.AccessToken = accessToken;
                x.AccessTokenType = accessTokenType;
            });
        }

        public AuthorizationBuilder AccessTokenType(string accessTokenType)
        {
            return Update(x => x.AccessTokenType = accessTokenType);
        }
    }
}