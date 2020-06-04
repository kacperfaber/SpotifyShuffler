using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public SpotifyUser SpotifyUser { get; set; }

        [ForeignKey("SpotifyUser")]
        public Guid SpotifyUserId { get; set; }

        [InverseProperty("Owner")]
        public List<PlaylistPrototype> PlaylistPrototypes { get; set; }

        [InverseProperty("Owner")]
        public List<Playlist> Playlists { get; set; }

        [InverseProperty("AuthorizedUser")]
        public List<Authorization> Authorizations { get; set; }

        [InverseProperty("AuthorizedUser")]
        public List<ApiRequest> ApiRequests { get; set; }
    }
}