using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class PlaylistPrototype
    {
        [Key]
        public Guid Id { get; set; }

        public Operation Operation { get; set; }
        
        

        [ForeignKey("PrototypeDataId")]
        public PlaylistPrototypeData PrototypeData { get; set; }

        public Guid PrototypeDataId { get; set; }

        [InverseProperty("PlaylistPrototype")]
        public List<TrackPrototype> Tracks { get; set; }
    }
}