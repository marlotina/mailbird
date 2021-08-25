using System.Collections.Generic;

namespace Reading.Mails.Api.Integration.Test.Model
{
    public class EmailList 
    {
        public int TotalEmails { get; set; }
        public List<EmailItem> List { get; set; }

        public EmailList()
        {
            this.List = new List<EmailItem>();
        }
    }
}
