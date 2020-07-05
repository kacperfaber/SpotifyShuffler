using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IRegistrationValidator
    {
        bool Validate(Registration registration);
    }
}