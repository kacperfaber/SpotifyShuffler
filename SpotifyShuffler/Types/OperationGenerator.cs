using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class OperationGenerator : IOperationGenerator
    {
        public IDefaultOperationKindGenerator DefaultOperationKindGenerator;
        public IDefaultPlaylistNameGenerator DefaultPlaylistNameGenerator;
        public IDefaultPlaylistDescriptionGenerator DefaultPlaylistDescriptionGenerator;

        public OperationGenerator(IDefaultOperationKindGenerator defaultOperationKindGenerator, IDefaultPlaylistDescriptionGenerator defaultPlaylistDescriptionGenerator, IDefaultPlaylistNameGenerator defaultPlaylistNameGenerator)
        {
            DefaultOperationKindGenerator = defaultOperationKindGenerator;
            DefaultPlaylistDescriptionGenerator = defaultPlaylistDescriptionGenerator;
            DefaultPlaylistNameGenerator = defaultPlaylistNameGenerator;
        }

        public Operation Generate(User user, SpotifyPlaylist playlist)
        {
            DateTime createdAt = DateTime.Now;
            
            return new Operation
            {
                Id = Guid.NewGuid(),
                Kind = DefaultOperationKindGenerator.Generate(),
                CreatedAt = createdAt,
                OriginalPlaylistDescription = playlist.Description,
                OriginalPlaylistName = playlist.Name,
                PlaylistName = DefaultPlaylistNameGenerator.Generate(createdAt,playlist),
                PlaylistDescription = DefaultPlaylistDescriptionGenerator.Generate(createdAt,playlist),
                OwnerId = user.Id,
                OriginalPlaylistId = playlist.Id
            };
        }
    }
}