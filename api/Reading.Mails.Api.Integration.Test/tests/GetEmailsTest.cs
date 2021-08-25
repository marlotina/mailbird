using Newtonsoft.Json;
using NUnit.Framework;
using Reading.Mails.Api.Integration.Test.Helper;
using Reading.Mails.Api.Integration.Test.Model;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Reading.Mails.Api.Integration.Test.tests
{
    [TestFixture]
    public class GetEmailsTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase("POP3",110, "STARTTLS", 0, 10)]
        [TestCase("POP3", 995, "SSL_TLS", 0, 10)]
        [TestCase("POP3", 110, "UNENCRYPTED", 0, 10)]
        [TestCase("IMAP", 143, "STARTTLS", 0, 10)]
        [TestCase("IMAP", 993, "SSL_TLS", 0, 10)]
        [TestCase("IMAP", 143, "UNENCRYPTED", 0, 10)]
        public void GetEmailsOK(string serverType, int port, string encryption, int index, int items)
        {
            var response = GetEmailsCall(serverType, port, encryption, index, items);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            var emails = JsonConvert.DeserializeObject<EmailList>(response.Content);
            Assert.IsTrue(emails.List.Any() && emails.List.Count == items);
        }

        [Test]
        [TestCase("POP3", 110, "STARTTLS", 0, 20)]
        [TestCase("POP3", 995, "SSL_TLS", 0, 15)]
        [TestCase("POP3", 110, "UNENCRYPTED", 0, 11)]
        [TestCase("IMAP", 143, "STARTTLS", 0, 8)]
        [TestCase("IMAP", 993, "SSL_TLS", 0, 6)]
        [TestCase("IMAP", 143, "UNENCRYPTED", 0, 17)]
        public void GetAllEmailsOK(string serverType, int port, string encryption, int index, int items)
        {
            var totalMails = 0;
            var finish = false;
            var emailList = new List<EmailItem>();
            while (!finish)
            {
                var response = GetEmailsCall(serverType, port, encryption, index, items);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                var emails = JsonConvert.DeserializeObject<EmailList>(response.Content);
                if (emails.List.Count > 0)
                {
                    for (var i = 0; i < emails.List.Count; i++)
                    {
                        emailList.Add(emails.List[i]);
                    }
                }

                index += items;
                finish = index >= emails.TotalEmails;
                totalMails = emails.TotalEmails;
            }

            Assert.IsTrue(totalMails == emailList.Count);
        }

        [Test]
        [TestCase("POP3", 995, "STARTTLS", 0, 10)]
        //[TestCase("POP3", 234, "SSL_TLS", 0, 10)]
        //[TestCase("POP3", 110, "SSL_TLS", 0, 10)]
        //[TestCase("IMAP", 993, "STARTTLS", 0, 10)]
        //[TestCase("IMAP", 143, "SSL_TLS", 0, 10)]
        //[TestCase("IMAP", 993, "UNENCRYPTED", 0, 10)]
        public void GetEmailsKO(string serverType, int port, string encryption, int index, int items)
        {
            var response = GetEmailsCall(serverType, port, encryption, index, items);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.InternalServerError);
        }

        [Test]
        [TestCase("", 995, "STARTTLS", 0, 10)]
        [TestCase("notype", 993, "SSL_TLS", 0, 10)]
        [TestCase("POP3", 0, "SSL_TLS", 0, 10)]
        [TestCase("IMAP", 993, "NOTYPE", 0, 10)]
        [TestCase("IMAP", 143, "", 0, 0)]
        [TestCase("34534", 993, "UNENCRYPTED", 0, 10)]
        public void GetEmailsBadRequest(string serverType, int port, string encryption, int index, int items)
        {
            var response = GetEmailsCall(serverType, port, encryption, index, items);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
            var errors = JsonConvert.DeserializeObject<IEnumerable<string>>(response.Content);
            Assert.IsTrue(errors.Any());
        }

        private static IRestResponse<EmailList> GetEmailsCall(string serverType, int port, string encryption, int index, int items)
        {
            var client = new RestClient(SeetingsHelper.API_URL);
            var request = new RestRequest("api/email/GetEmails", Method.GET);

            request.AddParameter("serverType", serverType);
            request.AddParameter("server", SeetingsHelper.SERVER);
            request.AddParameter("port", port);
            request.AddParameter("encryption", encryption);
            request.AddParameter("username", SeetingsHelper.USER_NAME);
            request.AddParameter("password", SeetingsHelper.USER_PASS);
            request.AddParameter("index", index);
            request.AddParameter("items", items);

            var response = client.Execute<EmailList>(request);
            return response;
        }
    }
}
