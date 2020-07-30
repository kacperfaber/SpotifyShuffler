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

        public EmailAddressManager(IConfirmationCodeSender confirmationCodeSender, IConfirmationCodeProvider confirmationCodeProvider,
            IConfirmationCodeValidator confirmationCodeValidator, IConfirmationCodeGenerator confirmationCodeGenerator,
            IEmailAddressGenerator emailAddressGenerator, IEmailAddressProvider emailAddressProvider, SpotifyContext spotifyContext,
            IEmailAddressDeleter emailAddressDeleter, IEmailAddressConfirmator emailAddressConfirmator, ISpotifyEmailIsSameChecker spotifyEmailIsSameChecker)
        {
            ConfirmationCodeSender = confirmationCodeSender;
            ConfirmationCodeProvider = confirmationCodeProvider;
            ConfirmationCodeValidator = confirmationCodeValidator;
            ConfirmationCodeGenerator = confirmationCodeGenerator;
            EmailAddressGenerator = emailAddressGenerator;
            EmailAddressProvider = emailAddressProvider;
            SpotifyContext = spotifyContext;
            EmailAddressDeleter = emailAddressDeleter;
            EmailAddressConfirmator = emailAddressConfirmator;
            SpotifyEmailIsSameChecker = spotifyEmailIsSameChecker;
        }

        public async Task<EmailAddressResult> CreateEmail(User user, string email)
        {
            // TODO Validate which email is taken.

            EmailAddress emailAddress = EmailAddressGenerator.Generate(user, email);

            await SpotifyContext.AddRangeAsync(emailAddress);
            await SpotifyContext.SaveChangesAsync();

            return EmailAddressResult.Created;
        }

        public async Task<EmailAddressResult> Confirm(string email, string code)
        {
            ConfirmationCode confirmationCode = ConfirmationCodeProvider.Provide(email, code);
            bool isValid = await ConfirmationCodeValidator.ValidateAsync(confirmationCode);

            if (isValid)
            {
                EmailAddress emailAddress = EmailAddressProvider.Provide(email);
                await EmailAddressConfirmator.ConfirmAsync(emailAddress, EmailConfirmationMethod.ConfirmationCode);

                return EmailAddressResult.Confirmed;
            }

            return EmailAddressResult.BadCode;
        }

        public async Task<EmailAddress> GetAsync(User user)
        {
            return await Task.Run(() => EmailAddressProvider.Provide(user));
        }

        public async Task DeleteAsync(User owner)
        {
            EmailAddress emailAddress = EmailAddressProvider.Provide(owner);
            await EmailAddressDeleter.DeleteAsync(emailAddress);
        }

        public async Task<EmailAddressResult> SendConfirmationLink(User user, string email)
        {
            EmailAddress emailAddress = EmailAddressProvider.Provide(user);

            if (emailAddress == null)
            {
                return EmailAddressResult.MissingEmail;
            }

            else if (emailAddress.IsDeleted)
            {
                return EmailAddressResult.Deleted;
            }

            else if (emailAddress.IsDeactivated)
            {
                return EmailAddressResult.Deactivated;
            }

            else if (emailAddress.IsConfirmed)
            {
                return EmailAddressResult.Confirmed;
            }

            if (user.SpotifyAccount != null)
            {
                if (SpotifyEmailIsSameChecker.Check(emailAddress, user.SpotifyAccount))
                {
                    await EmailAddressConfirmator.ConfirmAsync(emailAddress, EmailConfirmationMethod.Spotify);
                }
            }

            ConfirmationCode confirmationCode = ConfirmationCodeGenerator.Generate(email);
            await ConfirmationCodeSender.SendAsync(confirmationCode);
            
            await SpotifyContext.AddAsync(confirmationCode);
            await SpotifyContext.SaveChangesAsync();

            return EmailAddressResult.CodeSent;
        }
    }
}