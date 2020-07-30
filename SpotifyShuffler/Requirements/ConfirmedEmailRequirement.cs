using Microsoft.AspNetCore.Authorization;

namespace SpotifyShuffler
{
    public class ConfirmedEmailRequirement : AuthorizeAttribute, IAuthorizationRequirement
    {
    }
}