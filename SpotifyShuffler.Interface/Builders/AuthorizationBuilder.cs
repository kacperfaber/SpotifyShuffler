using SpotifyShuffler.Interface.Types;

namespace SpotifyShuffler.Interface.Builders
{
    public class AuthorizationBuilder : Builder<Authorization, AuthorizationBuilder>
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