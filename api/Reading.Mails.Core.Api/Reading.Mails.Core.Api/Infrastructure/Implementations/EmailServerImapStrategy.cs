using MailKit;
using MailKit.Net.Imap;
using Reading.Mails.Core.Api.Domain.Dto;
using Reading.Mails.Core.Api.Domain.enumerations;
using Reading.Mails.Core.Api.Infrastructure.Implementations.Base;
using Reading.Mails.Core.Api.Infrastructure.Contracts;
using System;
using System.Threading.Tasks;
using Reading.Mails.Core.Api.Domain.Model;
using System.Linq;
using Reading.Mails.Core.Api.Infrastructure.Helper;

namespace Reading.Mails.Core.Api.Infrastructure.Implementations
{
    public class EmailServerImapStrategy : EmailServerTypeStrategyBase, IEmailServerTypeStrategy
    {
        protected override ServerTypesEnum EmailServerType => ServerTypesEnum.IMAP;

        public override async Task<EmailBody> GetEmailBody(EmailBodyPetition emailConfiguration)
        {
            var email = default(EmailBody);

            using (var client = GetClientConnected(emailConfiguration))
            {
                try
                {
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    var message = await inbox.GetMessageAsync(new UniqueId(Convert.ToUInt32(emailConfiguration.EmailId)));
                    email = this.MapBody(message);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }

            return email;
        }

        public override async Task<EmailList> GetEmails(EmailListPetition emailConfiguration)
        {
            var emailList = new EmailList();
            
            using (var client = GetClientConnected(emailConfiguration))
            {
                try
                {
                    client.Inbox.Open(FolderAccess.ReadOnly);
                    emailList.TotalEmails = client.Inbox.Count;

                    var start = emailConfiguration.GetInverseStartItem(client.Inbox.Count);
                    var end = emailConfiguration.GetInverseEndItem(client.Inbox.Count) - 1;

                    var messages = await client.Inbox.FetchAsync(start, end,
                        MessageSummaryItems.Envelope | MessageSummaryItems.UniqueId | MessageSummaryItems.Flags);

                    for (var i=messages.Count-1; i>=0; i--)
                    {
                        emailList.List.Add(MapMessage(messages[i]));
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }

            return emailList;
        }

        private static ImapClient GetClientConnected(EmailProviderConfig emailConfiguration)
        {
            var client = new ImapClient
            {
                Timeout = 15000
            };

            client.Connect(emailConfiguration.Server, emailConfiguration.Port, 
                EncryptionSocketMailKitConversor.GetSocketOption(emailConfiguration.Encryption));

            client.Authenticate(emailConfiguration.Username, emailConfiguration.Password);

            return client;
        }

        private static EmailItem MapMessage(IMessageSummary message)
        {
            return new EmailItem
            {
                Id = message.UniqueId.Id.ToString(),
                To = message.Envelope.To.Mailboxes.Select(x => new AddressItem { Name = x.Name, Email = x.Address }),
                From = message.Envelope.From.Mailboxes.Select(x => new AddressItem { Name = x.Name, Email = x.Address }),
                Subject = message.Envelope.Subject,
                Date = message.Envelope.Date.HasValue ? message.Envelope.Date.Value.DateTime : null,
                ExtraInfo = new EmailExtraInfo { IsRead = message.Flags.Value.HasFlag(MessageFlags.Seen) }
            };
        }
    }
}
