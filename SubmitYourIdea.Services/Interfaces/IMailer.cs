using SubmitYourIdea.Services.Common;

namespace SubmitYourIdea.Services.Interfaces;

public interface IMailer
{
    Task SendEmailAsync(EmailPayload emailData);

}