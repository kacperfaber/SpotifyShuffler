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
        public OperationContext OperationContext;

        public OperationManager(OperationContext operationContext)
        {
            OperationContext = operationContext;
        }

        public async Task CreateAsync(Operation operation)
        {
            operation.Id ??= Guid.NewGuid();

            await OperationContext.AddAsync(operation);
            await OperationContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Operation operation)
        {
            OperationContext.Update(operation);
            await OperationContext.SaveChangesAsync();
        }

        public async Task<Operation> GetAsync(Guid operationId)
        {
            return await OperationContext.Operations
                .Include(x => x.Prototype)
                .ThenInclude(x => x.Tracks)
                .FirstOrDefaultAsync(x => x.Id == operationId);
        }

        public async Task<IEnumerable<SimpleOperation>> GetAsync(string originalPlaylistId)
        {
            IQueryable<Operation> operations = OperationContext.Operations.Where(x => x.OriginalPlaylistId == originalPlaylistId);
            return await operations.Cast<SimpleOperation>().ToListAsync();
        }

        public async Task<List<SimpleOperation>> GetAsync(User user)
        {
            IQueryable<Operation> operations = OperationContext.Operations.Where(x => x.OwnerId == user.Id);
            return await operations.Cast<SimpleOperation>().ToListAsync();
        }

        public async Task<SimpleOperation> GetSimpleAsync(Guid operationId)
        {
            return await OperationContext.Operations.FirstOrDefaultAsync(x => x.Id == operationId);
        }
    }
}