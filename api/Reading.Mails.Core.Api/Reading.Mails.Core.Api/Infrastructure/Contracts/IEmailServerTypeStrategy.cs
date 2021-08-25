using Reading.Mails.Core.Api.Domain.Dto;
using Reading.Mails.Core.Api.Domain.enumerations;
using Reading.Mails.Core.Api.Domain.Model;
using System.Threading.Tasks;

namespace Reading.Mails.Core.Api.Infrastructure.Contracts
{
    public interface IEmailServerTypeStrategy
    {
        bool Match(ServerTypesEnum serverType);

        Task<EmailList> GetEmails(EmailListPetition emailConfiguration);

        Task<EmailBody> GetEmailBody(EmailBodyPetition emailConfiguration);
    }
}
