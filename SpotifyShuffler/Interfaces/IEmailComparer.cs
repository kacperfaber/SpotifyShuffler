namespace SpotifyShuffler.Interfaces
{
    public interface IEmailComparer
    {
        bool Compare(string email1, string email2);
    }
}