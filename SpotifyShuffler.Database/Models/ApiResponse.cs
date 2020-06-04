using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class ApiResponse
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("RequestId")]
        public ApiRequest Request { get; set; }

        public Guid RequestId { get; set; }

        public DateTime ResponsedAt { get; set; }

        public int ResponsedStatusCode { get; set; }

        public string ResponseBody { get; set; }
    }
}