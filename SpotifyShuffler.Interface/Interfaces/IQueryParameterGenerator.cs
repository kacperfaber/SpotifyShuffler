using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public interface IQueryParameterGenerator
    {
        string Generate(KeyValuePair<string, object> keyValuePair);

        string Generate(string name, object value);
    }
}