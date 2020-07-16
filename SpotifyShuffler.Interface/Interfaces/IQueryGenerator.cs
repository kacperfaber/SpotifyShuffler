using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public interface IQueryGenerator
    {
        string Generate(string url, Dictionary<string, object> queryParameters);
    }
}