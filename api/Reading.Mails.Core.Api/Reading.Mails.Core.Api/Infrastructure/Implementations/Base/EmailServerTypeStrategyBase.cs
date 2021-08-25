using MimeKit;
using Reading.Mails.Core.Api.Domain.Dto;
using Reading.Mails.Core.Api.Domain.enumerations;
using Reading.Mails.Core.Api.Domain.Model;
using Reading.Mails.Core.Api.Infrastructure.Contracts;
using System.Threading.Tasks;

namespace Reading.Mails.Core.Api.Infrastructure.Implementations.Base
{
    public abstract class EmailServerTypeStrategyBase : IEmailServerTypeStrategy
    {
        protected virtual ServerTypesEnum EmailServerType { get; set; }

        public virtual bool Match(ServerTypesEnum serverType)
        {
            return (serverType == EmailServerType);
        }

        public abstract Task<EmailBody> GetEmailBody(EmailBodyPetition emailConfiguration);

        public abstract Task<EmailList> GetEmails(EmailListPetition emailConfiguration);      

        protected virtual EmailBody MapBody(MimeMessage message)
        {
            return new EmailBody
            {
                IsHtml = message.HtmlBody != null,
                Text = message.HtmlBody ?? message.TextBody
            };
        }
    }
}
