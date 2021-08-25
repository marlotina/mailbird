using Reading.Mails.Core.Api.Application.Contracts;
using Reading.Mails.Core.Api.Application.Exceptions;
using Reading.Mails.Core.Api.Domain.Dto;
using Reading.Mails.Core.Api.Domain.Model;
using Reading.Mails.Core.Api.Infrastructure.Contracts;
using Reading.Mails.Core.Api.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reading.Mails.Core.Api.Domain.enumerations;

namespace Reading.Mails.Core.Api.Application.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IEnumerable<IEmailServerTypeStrategy> iEmailProviderStrategies;

        public EmailService(IEnumerable<IEmailServerTypeStrategy> iEmailProviderStrategies)
        {
            this.iEmailProviderStrategies = iEmailProviderStrategies;
        }

        public async Task<EmailBody> GetEmailBody(EmailBodyPetition eMailDetailPetition)
        {
            var strategy = this.GetStrategies(eMailDetailPetition);
            return await strategy.GetEmailBody(eMailDetailPetition);
        }

        public async Task<EmailList> GetEmailList(EmailListPetition emailListPetition)
        {
            var strategy = this.GetStrategies(emailListPetition);
            return await strategy.GetEmails(emailListPetition);
        }

        public IEmailServerTypeStrategy GetStrategies(EmailProviderConfig emailProviderConfig)
        {
            var strategy = this.iEmailProviderStrategies.FirstOrDefault(x => x.Match(emailProviderConfig.ServerType.ToEnum<ServerTypesEnum>()));

            if (strategy == null)
                throw new NoFoundStrategy("Not found strategy for the server type");

            return strategy;
        }
    }
}
