using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class EmailAddressManager
    {
        public SpotifyContext SpotifyContext;
        public IEmailAddressProvider EmailAddressProvider;
        public IEmailAddressDeleter EmailAddressDeleter;
        public IEmailAddressGenerator EmailAddressGenerator;
        public IConfirmationCodeGenerator ConfirmationCodeGenerator;
        public IConfirmationCodeValidator ConfirmationCodeValidator;
        public IConfirmationCodeProvider ConfirmationCodeProvider;
        public IConfirmationCodeSender ConfirmationCodeSender;
        public ISpotifyEmailIsSameChecker SpotifyEmailIsSameChecker;
        public IEmailAddressConfirmator EmailAddressConfirmator;

        public EmailAddressManager(IConfirmationCodeSender confirmationCodeSender, IConfirmationCodeProvider confirmationCodeProvider, IConfirmationCodeValidator confirmationCodeValidator, IConfirmationCodeGenerator confirmationCodeGenerator, IEmailAddressGenerator emailAddressGenerator, IEmailAddressProvider emailAddressProvider, SpotifyContext spotifyContext)
        {
            ConfirmationCodeSender = confirmationCodeSender;
            ConfirmationCodeProvider = confirmationCodeProvider;
            ConfirmationCodeValidator = confirmationCodeValidator;
            ConfirmationCodeGenerator = confirmationCodeGenerator;
            EmailAddressGenerator = emailAddressGenerator;
            EmailAddressProvider = emailAddressProvider;
            SpotifyContext = spotifyContext;
        }

        public async Task<EmailAddressResult> CreateEmail(User user, string email)
        {
            // TODO Validate is taken.

            EmailAddress emailAddress = EmailAddressGenerator.Generate(user, email);
            ConfirmationCode confirmationCode = ConfirmationCodeGenerator.Generate(email);

            await ConfirmationCodeSender.SendAsync(confirmationCode);

            await SpotifyContext.AddRangeAsync(emailAddress, confirmationCode);
            await SpotifyContext.SaveChangesAsync();

            return EmailAddressResult.CodeSent;
        }

        public async Task<EmailAddressResult> Confirm(string email, string code)
        {
            ConfirmationCode confirmationCode = ConfirmationCodeProvider.Provide(email, code);
            bool isValid = await ConfirmationCodeValidator.ValidateAsync(confirmationCode);

            if (isValid)
            {
                EmailAddress emailAddress = EmailAddressProvider.Provide(email);

                if (emailAddress == null)
                {
                    return EmailAddressResult.MissingEmail;
                }

                emailAddress.IsConfirmed = true;
                emailAddress.ConfirmedAt = DateTime.Now;

                confirmationCode.IsUsed = true;
                confirmationCode.UsedAt = DateTime.Now;

                SpotifyContext.UpdateRange(emailAddress, confirmationCode);
                await SpotifyContext.SaveChangesAsync();

                return EmailAddressResult.Confirmed;
            }

            return EmailAddressResult.BadCode;
        }
    }
}