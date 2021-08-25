﻿using System.Linq;

namespace Reading.Mails.Core.Api.Domain.Model
{
    public class EmailBodyPetition : EmailProviderConfig
    {
        public EmailBodyPetition(string serverType, string server, int port, string encryption, string username, string password, int emailId) : 
            base(serverType, server, port, encryption, username, password)
        {
            this.EmailId = emailId;
        }

        public int EmailId { get; set; }

        protected override bool ValidateData()
        {
            if (this.EmailId < 0)
                this.LogErrors.Add("The emailId param is not valid.");

            return !this.LogErrors.Any();
        }
    }
}
