using Reading.Mails.Core.Api.Domain.enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using Reading.Mails.Core.Api.Extensions;

namespace Reading.Mails.Core.Api.Domain.Model
{
    public abstract class EmailProviderConfig
    {
        public string ServerType { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Encryption { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsSsl
        {
            get 
            {
                return this.Encryption.ToEnum<EncryptionTypesEnum>() != EncryptionTypesEnum.UNENCRYPTED;
            }
        }

        public List<string> LogErrors { get; set; }

        public EmailProviderConfig(string serverType, string server, int port, string encryption, string username, string password)
        {
            this.ServerType = serverType;
            this.Server = server;
            this.Port = port;
            this.Encryption = encryption;
            this.Username = username;
            this.Password = password;
            this.LogErrors = new List<string>();
        }

        public bool IsValidData() {
            return BaseValidateData() && ValidateData();
        }

        private bool BaseValidateData()
        {
            if (!Enum.TryParse(this.ServerType, true, out ServerTypesEnum _))
                this.LogErrors.Add("The server type param is not valid.");
            if (string.IsNullOrWhiteSpace(Server))
                this.LogErrors.Add("The server param is require.");
            if (Port == 0)
                this.LogErrors.Add("The port param is require.");
            if (!Enum.TryParse(this.Encryption, true, out EncryptionTypesEnum _))
                this.LogErrors.Add("The encryption param is not valid.");
            if (string.IsNullOrWhiteSpace(Username))
                this.LogErrors.Add("The username param is require.");
            if (string.IsNullOrWhiteSpace(Password))
                this.LogErrors.Add("The password param is require.");

            return !this.LogErrors.Any();
        }

        protected abstract bool ValidateData();
    }
}
