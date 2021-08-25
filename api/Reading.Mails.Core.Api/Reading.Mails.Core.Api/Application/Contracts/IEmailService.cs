using Reading.Mails.Core.Api.Domain.Dto;
using Reading.Mails.Core.Api.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reading.Mails.Core.Api.Application.Contracts
{
    public interface IEmailService
    {
        Task<EmailBody> GetEmailBody(EmailBodyPetition eMailDetailPetition);

        Task<EmailList> GetEmailList(EmailListPetition emailListPetition);
    }
}
