using System.Linq;

namespace Reading.Mails.Core.Api.Domain.Model
{
    public class EmailListPetition : EmailProviderConfig
    {
        public EmailListPetition(string serverType, string server, int port, string encryption, string username, string password, int index, int items) : 
            base(serverType, server, port, encryption, username, password)
        {
            this.Index = index;
            this.Items = items;
        }

        public int Index { get; set; }
        public int Items { get; set; }

        public int GetInverseStartItem(int totalEmails)
        {
            return this.GetInverseEndItem(totalEmails) < this.Items ? 0 : this.GetInverseEndItem(totalEmails) - this.Items;
        }

        public int GetInverseEndItem(int totalEmails)
        {
            return totalEmails - this.Index;
        }

        protected override bool ValidateData()
        {
            if (this.Index < 0)
                this.LogErrors.Add("The index param is not valid.");
            if (this.Items == 0)
                this.LogErrors.Add("The items param is not valid.");

            return !this.LogErrors.Any();
        }
    }
}
