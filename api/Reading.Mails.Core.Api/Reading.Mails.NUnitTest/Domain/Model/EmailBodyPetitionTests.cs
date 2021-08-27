using NUnit.Framework;
using Reading.Mails.Core.Api.Domain.Model;

namespace Reading.Mails.NUnitTest.Domain.Model
{
    [TestFixture]
    public class EmailBodyPetitionTests
    {
        [TestCase("IMAP", "imap.smtp", 143, "UNENCRYPTED", "test@gmail.com", "asdsa", "1235")]
        public void IsValidItem(string serverType, string server, int port, string encryption, string username, string password, string emailId)
        {
            var emailProviderConfig = new EmailBodyPetition(serverType, server, port, encryption, username, password, emailId);
            Assert.IsTrue(emailProviderConfig.IsValidData());
        }

        [TestCase("", "imap.smtp", 110, "STARTTLS", "test@gmail.com", "asdsa", "1235")]
        [TestCase("POP3", "", 110, "SSL_TLS", "test@gmail.com", "asdsa", "1235")]
        [TestCase("POP3", "imap.smtp", 0, "SSL_TLS", "test@gmail.com", "asdsa", "1235")]
        [TestCase("POP3", "imap.smtp", 110, "", "test@gmail.com", "asdsa", "1235")]
        [TestCase("IMAP", "imap.smtp", 143, "STARTTLS", "", "asdsa", "1235")]
        [TestCase("IMAP", "imap.smtp", 993, "SSL_TLS", "test@gmail.com", "", "1235")]
        public void NotValidItem(string serverType, string server, int port, string encryption, string username, string password, string emailId)
        {
            var emailProviderConfig = new EmailBodyPetition(serverType, server, port, encryption, username, password, emailId);
            Assert.IsTrue(!emailProviderConfig.IsValidData());
        }

    }
}


