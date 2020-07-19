using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpotifyShuffler.Interface
{
    public class UriValidator : IUriValidator
    {
        public async Task<bool> ValidateAsync(string uri)
        {
            return await Task.Run(() => Regex.IsMatch(uri, "^spotify:track:.+$"));
        }
    }
}