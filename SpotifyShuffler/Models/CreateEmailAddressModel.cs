namespace SpotifyShuffler.Models
{
    public class CreateEmailAddressModel : LayoutModel
    {
        public string Email { get; set; }
        public bool IsCodeSent { get; set; }
        public string ConfirmationCode { get; set; }
    }
}