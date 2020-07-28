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

            await SpotifyContext.AddAsync(operation);
            await SpotifyContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Operation operation)
        {
            SpotifyContext.Update(operation);
            await SpotifyContext.SaveChangesAsync();
        }

        public async Task<Operation> GetAsync(Guid operationId)
        {
            return await SpotifyContext.Operations.FirstOrDefaultAsync(x => x.Id == operationId);
        }

        public async Task<IEnumerable<SimpleOperation>> GetAsync(string originalPlaylistId)
        {
            IQueryable<Operation> operations = SpotifyContext.Operations.Where(x => x.OriginalPlaylistId == originalPlaylistId);
            return await operations.Cast<SimpleOperation>().ToListAsync();
        }

        public async Task<List<SimpleOperation>> GetAsync(User user)
        {
            IQueryable<Operation> operations = SpotifyContext.Operations.Where(x => x.OwnerId == user.Id);
            return await operations.Cast<SimpleOperation>().ToListAsync();
        }
    }
}