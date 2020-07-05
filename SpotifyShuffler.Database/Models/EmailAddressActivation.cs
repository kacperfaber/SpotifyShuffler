using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class EmailAddressActivation
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsActivated { get; set; }

        public DateTime? ActivatedAt { get; set; }

        [InverseProperty("EmailAddressActivation")]
        public List<ActivationCode> ActivationCodes { get; set; }

        [InverseProperty("EmailAddressActivation")]
        public List<SpotifyEmailValidation> SpotifyValidations { get; set; }
    }
}