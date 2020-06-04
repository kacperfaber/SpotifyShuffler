using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class Authorization
    {
        [Key]
        public Guid Id { get; set; }

        public User AuthorizedUser { get; set; }

        public DateTime RequestedAt { get; set; }

        public DateTime ResponsedAt { get; set; }
        
        public DateTime? AuthorizedAt { get; set; }

        public DateTime? ExpiredAt { get; set; }

        public DateTime? LastUsedAt { get; set; }

        public bool AuthorizedSuccessfully { get; set; }

        public bool HasAccessToken { get; set; }

        public string AccessToken { get; set; }

        public string ResponseJson { get; set; }

        [InverseProperty("Authorization")]
        public List<ApiRequest> Requests { get; set; }
    }
}