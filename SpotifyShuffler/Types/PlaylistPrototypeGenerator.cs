﻿using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistPrototypeGenerator : IPlaylistPrototypeGenerator
    {
        public IPlaylistPrototypeDataGenerator DataGenerator;
        public ITrackPrototypesGenerator PrototypesGenerator;
        public ITrackPrototypesGenerator TrackPrototypesGenerator;

        public PlaylistPrototypeGenerator(IPlaylistPrototypeDataGenerator dataGenerator, ITrackPrototypesGenerator trackPrototypesGenerator)
        {
            DataGenerator = dataGenerator;
            TrackPrototypesGenerator = trackPrototypesGenerator;
        }

        public async Task<PlaylistPrototype> GenerateAsync(SpotifyPlaylist playlist, Operation operation, string playlistName, string playlistDescription)
        {
            PlaylistPrototypeData data = await DataGenerator.GenerateAsync(playlistName, playlistDescription);

            return new PlaylistPrototype
            {
                Id = Guid.NewGuid(),
                Operation = operation,
                PrototypeData = data,
                Tracks = await TrackPrototypesGenerator.GenerateAsync(playlist)
            };
        }
    }
}