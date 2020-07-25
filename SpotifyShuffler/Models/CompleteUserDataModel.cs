using System;
using System.ComponentModel.DataAnnotations;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Models
{
    public class CompleteUserDataModel : LayoutModel
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [MaxLength(26)]
        [MinLength(3)]
        public string UserName { get; set; }

        public SpotifyAccount SpotifyAccount { get; set; }

        public Guid RegistrationId { get; set; }

        public string SpotifyId { get; set; }
    }
}