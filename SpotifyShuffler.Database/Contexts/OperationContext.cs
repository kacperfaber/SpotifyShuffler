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

        public DbSet<Operation> Operations { get; set; }
        public DbSet<TrackPrototype> TrackPrototypes { get; set; }
        public DbSet<PlaylistPrototype> PlaylistPrototype { get; set; }
    }
}