using System.Collections.Generic;
using System.Reflection;

namespace SpotifyShuffler.Interface
{
    public class InstanceToDictionaryConverter : IInstanceToDictionaryConverter
    {
        public Dictionary<string, object> Convert(object instance, bool toLower = true)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach (PropertyInfo property in instance.GetType().GetProperties())
                dictionary.Add(toLower ? property.Name.ToLower() : property.Name, property.GetValue(instance));

            return dictionary;
        }
    }
}