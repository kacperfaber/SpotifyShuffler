using Microsoft.EntityFrameworkCore;

namespace SpotifyShuffler.Database
{
    public class OperationContext : DbContext
    {
        public OperationContext(DbContextOptions options) : base(options)
        {
        }

        public OperationContext()
        {
        }
    }
}