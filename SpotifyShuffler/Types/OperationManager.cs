using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Types
{
    public class OperationManager
    {
        public SpotifyContext SpotifyContext;

        public OperationManager(SpotifyContext spotifyContext)
        {
            SpotifyContext = spotifyContext;
        }

        public async Task CreateAsync(Operation operation)
        {
            operation.Id ??= Guid.NewGuid();
            
            await SpotifyContext.Operations.AddAsync(operation);
            await SpotifyContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Operation operation)
        {
            SpotifyContext.Operations.Update(operation);
            await SpotifyContext.SaveChangesAsync();
        }

        public async Task<Operation> GetAsync(Guid operationId)
        {
            return await SpotifyContext.Operations
                .Include(x => x.Prototype)
                .ThenInclude(x => x.Tracks)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == operationId);
        }

        public async Task<List<Operation>> GetAsync(string originalPlaylistId)
        {
            return await SpotifyContext.Operations.Where(x => x.OriginalPlaylistId == originalPlaylistId).ToListAsync();
        }
    }
}