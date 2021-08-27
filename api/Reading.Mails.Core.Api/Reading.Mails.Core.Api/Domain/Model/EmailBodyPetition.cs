using System.Linq;

namespace Reading.Mails.Core.Api.Domain.Model
{
    public class EmailBodyPetition : EmailProviderConfig
    {
        public EmailBodyPetition(string serverType, string server, int port, string encryption, string username, string password, string emailId) : 
            base(serverType, server, port, encryption, username, password)
        {
            this.EmailId = emailId;
        }

        public string EmailId { get; set; }

        protected override bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(this.EmailId))
                this.LogErrors.Add("The emailId param is not valid.");

            return !this.LogErrors.Any();
        }
    }
}
