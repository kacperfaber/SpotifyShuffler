using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IRegistrationGenerator
    {
        Registration GenerateRegistration(SpotifyAccount account);
    }
}