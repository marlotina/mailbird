using MailKit.Net.Pop3;
using MimeKit;
using Reading.Mails.Core.Api.Domain.Dto;
using Reading.Mails.Core.Api.Domain.enumerations;
using Reading.Mails.Core.Api.Domain.Model;
using Reading.Mails.Core.Api.Infrastructure.Contracts;
using Reading.Mails.Core.Api.Infrastructure.Implementations.Base;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Reading.Mails.Core.Api.Infrastructure.Helper;

namespace Reading.Mails.Core.Api.Infrastructure.Implementations
{
    public class EmailServerPopStrategy : EmailServerTypeStrategyBase, IEmailServerTypeStrategy
    {
        protected override ServerTypesEnum EmailServerType => ServerTypesEnum.POP3;

        public override async Task<EmailBody> GetEmailBody(EmailBodyPetition emailConfiguration)
        {
            var email = default(EmailBody);

            using (var client = GetClientConnected(emailConfiguration))
            {
                try
                {
                    var uidList = client.GetMessageUids();
                    int indexEmail = uidList.IndexOf(emailConfiguration.EmailId);

                    var message = await client.GetMessageAsync(indexEmail);
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
                    emailList.TotalEmails = client.Count;

                    var start = emailConfiguration.GetInverseStartItem(client.Count);
                    var end = emailConfiguration.GetInverseEndItem(client.Count) - start;
                    
                    var indexList = GenerateEmailsIndexArray(start, end);
                    var messages = await client.GetMessagesAsync(indexList);
                    var uidList = client.GetMessageUids();

                    for (var i=messages.Count-1; i>=0; i--)
                    {
                        var uid = uidList[indexList[i]];
                        emailList.List.Add(MapMessage(uid, messages[i]));
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

        private static int[] GenerateEmailsIndexArray(int startIndex, int endIndex)
        {
            var value = startIndex;
            return Enumerable
                .Repeat(0, endIndex)
                .Select((tr) => tr = value++)
                .ToArray();
        }

        private static Pop3Client GetClientConnected(EmailProviderConfig emailConfiguration)
        {
            var client = new Pop3Client
            {
                Timeout = 15000
            };

            client.Connect(emailConfiguration.Server, emailConfiguration.Port, 
                EncryptionSocketMailKitConversor.GetSocketOption(emailConfiguration.Encryption));

            client.Authenticate(emailConfiguration.Username, emailConfiguration.Password);

            return client;
        }

        private static EmailItem MapMessage(string emailId, MimeMessage mimeMessage)
        {
            return new EmailItem
            {
                Id = emailId,
                To = mimeMessage.To.Mailboxes.Select(x => new AddressItem { Name = x.Name, Email = x.Address }),
                From = mimeMessage.From.Mailboxes.Select(x => new AddressItem { Name = x.Name, Email = x.Address }),
                Subject = mimeMessage.Subject,
                Date = mimeMessage.Date.UtcDateTime
            };
        }
    }
}
