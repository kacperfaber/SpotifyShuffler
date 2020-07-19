using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public interface IUriFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> uris);
    }
}