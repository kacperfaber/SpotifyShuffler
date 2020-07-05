using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class RegistrationActivator : IRegistrationActivator
    {
        public void Activate(Registration registration)
        {
            registration.ActivatedAt = DateTime.Now;
        }
    }
}