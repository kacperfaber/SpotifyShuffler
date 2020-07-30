using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SpotifyShuffler.Database;
using SpotifyShuffler.Types;

namespace SpotifyShuffler
{
    public class RequireConfirmedEmailHandler : AuthorizationHandler<ConfirmedEmailRequirement>
    {
        public UserManager UserManager;
        public EmailAddressManager EmailAddressManager;

        public RequireConfirmedEmailHandler(EmailAddressManager emailAddressManager, UserManager userManager)
        {
            EmailAddressManager = emailAddressManager;
            UserManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ConfirmedEmailRequirement requirement)
        {
            User user = await UserManager.GetUserAsync(context.User);
            EmailAddress emailAddress = await EmailAddressManager.GetAsync(user);

            if (emailAddress != null)
            {
                if (emailAddress.IsConfirmed)
                {
                    context.Succeed(requirement);
                }

                else
                {
                    context.Fail();
                }
            }

            else
            {
                context.Fail();
            }
        }
    }
}