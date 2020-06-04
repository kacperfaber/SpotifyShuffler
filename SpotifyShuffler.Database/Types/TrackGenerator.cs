using System;
using System.Collections.Generic;
using System.Linq;
using SpotifyShuffler.Database.Interfaces;
using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Database.Types
{
    public class TrackGenerator : ITrackGenerator
    {
        public IPrimaryArtistGenerator PrimaryArtistGenerator;

        public TrackGenerator(IPrimaryArtistGenerator primaryArtistGenerator)
        {
            PrimaryArtistGenerator = primaryArtistGenerator;
        }

        public Track GenerateTrack(SpotifyTrack spotifyTrack)
        {
            PrimaryArtist primaryPrimaryArtist = PrimaryArtistGenerator.GeneratePrimaryArtist(spotifyTrack.Artists);

            return new Track()
            {
                SpotifyId = spotifyTrack.Id,
                PrimaryArtist = primaryPrimaryArtist,
                Name = spotifyTrack.Name,
                GeneratedAt = DateTime.Now,
                DurationMilliseconds = spotifyTrack.DurationMs
            };
        }
    }
}