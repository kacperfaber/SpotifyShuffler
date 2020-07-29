using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ConfirmationCodeValidator : IConfirmationCodeValidator
    {
        public async Task<bool> ValidateAsync(ConfirmationCode confirmationCode)
        {
            return await Task.Run(() => confirmationCode != null);
        }
    }
}