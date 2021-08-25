using NUnit.Framework;
using Reading.Mails.Core.Api.Domain.Model;

namespace Reading.Mails.NUnitTest.Domain.Model
{
    [TestFixture]
    public class EmailListPetitionTests
    {
        [TestCase("IMAP", "imap.smtp", 143, "UNENCRYPTED", "test@gmail.com", "asdsa", 0, 10)]
        public void IsValidItem(string serverType, string server, int port, string encryption, string username, string password, int index, int items)
        {
            var emailProviderConfig = new EmailListPetition(serverType, server, port, encryption, username, password, index, items);
            Assert.IsTrue(emailProviderConfig.IsValidData());
        }

        [TestCase("", "imap.smtp", 110, "STARTTLS", "test@gmail.com", "asdsa", 0, 10)]
        [TestCase("POP3", "", 110, "SSL_TLS", "test@gmail.com", "asdsa", 0, 10)]
        [TestCase("POP3", "imap.smtp", 0, "SSL_TLS", "test@gmail.com", "asdsa", 0, 10)]
        [TestCase("POP3", "imap.smtp", 110, "", "test@gmail.com", "asdsa", 0, 10)]
        [TestCase("IMAP", "imap.smtp", 143, "STARTTLS", "", "asdsa", 0, 10)]
        [TestCase("IMAP", "imap.smtp", 993, "SSL_TLS", "test@gmail.com", "", 0, 10)]
        [TestCase("IMAP", "imap.smtp", 143, "UNENCRYPTED", "test@gmail.com", "asdsa", 0, 0)]
        public void NotValidItem(string serverType, string server, int port, string encryption, string username, string password, int index, int items)
        {
            var emailProviderConfig = new EmailListPetition(serverType, server, port, encryption, username, password, index, items);
            Assert.IsTrue(!emailProviderConfig.IsValidData());
        }

        [TestCase(0, 10, 47, 37)]
        [TestCase(10, 10, 47, 27)]
        [TestCase(20, 10, 47, 17)]
        [TestCase(30, 10, 47, 7)]
        [TestCase(40, 10, 47, 0)]
        public void GetInverseStartItemTest(int index, int items, int totalItems, int expectValue)
        {
            var emailProviderConfig = new EmailListPetition("POP3", "imap.server", 110, "SSL_TLS", "test@gmail.com", "asdsa", index, items);
            var result = emailProviderConfig.GetInverseStartItem(totalItems);
            Assert.IsTrue(result == expectValue);
        }

        [TestCase(0, 10, 47, 47)]
        [TestCase(10, 10, 47, 37)]
        [TestCase(20, 10, 47, 27)]
        [TestCase(30, 10, 47, 17)]
        [TestCase(40, 10, 47, 7)]
        public void GetInverseEndItemtest(int index, int items, int totalItems, int expectValue)
        {
            var emailProviderConfig = new EmailListPetition("POP3", "imap.server", 110, "SSL_TLS", "test@gmail.com", "asdsa", index, items);
            var result = emailProviderConfig.GetInverseEndItem(totalItems);
            Assert.IsTrue(result == expectValue);
        }
    }
}


