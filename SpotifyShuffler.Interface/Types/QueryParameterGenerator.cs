using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public class QueryParameterGenerator : IQueryParameterGenerator
    {
        public string Generate(KeyValuePair<string, object> keyValuePair)
        {
            return $"{keyValuePair.Key}={keyValuePair.Value}";
        }

        public string Generate(string name, object value)
        {
            return $"{name}={value}";
        }
    }
}