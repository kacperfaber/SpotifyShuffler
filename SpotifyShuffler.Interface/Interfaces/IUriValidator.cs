using System.Threading.Tasks;

namespace SpotifyShuffler.Interface
{
    public interface IUriValidator
    {
        Task<bool> ValidateAsync(string uri);
    }
}