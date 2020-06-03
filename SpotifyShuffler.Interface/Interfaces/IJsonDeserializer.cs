namespace SpotifyShuffler.Interface.Interfaces
{
    public interface IJsonDeserializer
    {
        T Deserialize<T>(string json);
    }
}