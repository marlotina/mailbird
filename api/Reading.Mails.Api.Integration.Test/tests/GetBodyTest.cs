using Newtonsoft.Json;
using NUnit.Framework;
using Reading.Mails.Api.Integration.Test.Helper;
using Reading.Mails.Api.Integration.Test.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Reading.Mails.Api.Integration.Test.tests
{
    [TestFixture]
    public class GetBodyTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase("POP3", 110, "STARTTLS", 7170)]
        [TestCase("POP3", 995, "SSL_TLS", 7170)]
        [TestCase("POP3", 110, "UNENCRYPTED", 7170)]
        [TestCase("IMAP", 143, "STARTTLS", 7170)]
        [TestCase("IMAP", 993, "SSL_TLS", 7170)]
        [TestCase("IMAP", 143, "UNENCRYPTED", 7170)]
        public void GetAllEmailsOK(string serverType, int port, string encryption, int emailId)
        {
            var response = GetEmailsCall(serverType, port, encryption, emailId);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            var body = JsonConvert.DeserializeObject<EmailBody>(response.Content);

            Assert.IsTrue(body != null && !string.IsNullOrEmpty(body.Text));
        }

        [Test]
        [TestCase("POP3", 995, "STARTTLS", 10)]
        //[TestCase("POP3", 234, "SSL_TLS", 0, 10)]
        //[TestCase("POP3", 110, "SSL_TLS", 0, 10)]
        //[TestCase("IMAP", 993, "STARTTLS", 0, 10)]
        //[TestCase("IMAP", 143, "SSL_TLS", 0, 10)]
        //[TestCase("IMAP", 993, "UNENCRYPTED", 0, 10)]
        public void GetEmailsKO(string serverType, int port, string encryption, int emailId)
        {
            var response = GetEmailsCall(serverType, port, encryption, emailId);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.InternalServerError);
        }

        [Test]
        [TestCase("", 995, "STARTTLS", 0)]
        [TestCase("notype", 993, "SSL_TLS", 6545)]
        [TestCase("POP3", 0, "SSL_TLS", 6545)]
        [TestCase("IMAP", 993, "NOTYPE", 0)]
        [TestCase("IMAP", 143, "", 0)]
        [TestCase("34534", 993, "UNENCRYPTED", 4565)]
        public void GetEmailsBadRequest(string serverType, int port, string encryption, int emailId)
        {
            var response = GetEmailsCall(serverType, port, encryption, emailId);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
            var errors = JsonConvert.DeserializeObject<IEnumerable<string>>(response.Content);
            Assert.IsTrue(errors.Any());
        }

        private static IRestResponse<EmailList> GetEmailsCall(string serverType, int port, string encryption, int emailId)
        {
            var client = new RestClient(SeetingsHelper.API_URL);
            var request = new RestRequest("api/email/GetEmaillBody", Method.GET);

            request.AddParameter("serverType", serverType);
            request.AddParameter("server", SeetingsHelper.SERVER);
            request.AddParameter("port", port);
            request.AddParameter("encryption", encryption);
            request.AddParameter("username", SeetingsHelper.USER_NAME);
            request.AddParameter("password", SeetingsHelper.USER_PASS);
            request.AddParameter("emailId", emailId);

            var response = client.Execute<EmailList>(request);
            return response;
        }
    }
}
