using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ConfirmationCodeSender : IConfirmationCodeSender
    {
        public IConfiguration Configuration;
        
        
        
        public Task SendAsync(ConfirmationCode confirmationCode)
        {
            // return Task.Run(() =>
            // {
            //     SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            //     {
            //         UseDefaultCredentials = false,
            //         Credentials = new NetworkCredential("noconkrystian484", ***),
            //         DeliveryMethod = SmtpDeliveryMethod.Network,
            //         EnableSsl = true
            //     };
            //
            //     MailAddress senderAddress = new MailAddress();
            //     MailAddress receiverAddress = new MailAddress(confirmationCode.Email);
            //
            //     MailMessage message = new MailMessage(senderAddress, receiverAddress)
            //     {
            //         IsBodyHtml = true,
            //         Body = $"Hello, Your confirmation code is <strong>{confirmationCode.Code}</strong>",
            //         Subject = "Confirm your email!"
            //     };
            //
            //     client.Send(message);
            // });

            return Task.Run(() => { });
        }
    }
}