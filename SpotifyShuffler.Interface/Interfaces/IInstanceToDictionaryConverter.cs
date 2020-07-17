using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public interface IInstanceToDictionaryConverter
    {
        Dictionary<string, object> Convert(object instance, bool toLower = true);
    }
}