using System;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailComparer : IEmailComparer
    {
        public bool Compare(string email1, string email2)
        {
            return email1.Equals(email2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}