using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class Operation : SimpleOperation
    {
        [ForeignKey("PlaylistPrototypeId")]
        public PlaylistPrototype Prototype { get; set; }

        public Guid? PlaylistPrototypeId { get; set; }
    }
}