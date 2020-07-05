using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class RegistrationValidator : IRegistrationValidator
    {
        public bool Validate(Registration registration)
        {
            if (registration == null)
            {
                return false;
            }

            return registration.ActivatedAt != null && registration.UserCreatedAt == null;
        }
    }
}