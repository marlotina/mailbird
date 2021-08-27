using Reading.Mails.Core.Api.Api.Model.Request;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Reading.Mails.Core.Api.Api.services
{
    public static class RequestCredentilsService
    {
        public static CredentialHeaderRequest GetCredentialsFromHeader(string authorization)
        {
            var credentialsRequest = new CredentialHeaderRequest();
            if (authorization != null)
            {
                var authHeaderValue = AuthenticationHeaderValue.Parse(authorization);
                if (authHeaderValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty))
                                        .Split(':');
                    if (credentials.Length == 2)
                    {
                        credentialsRequest.Username = credentials[0];
                        credentialsRequest.Password = credentials[1];
                    }
                }
            }

            return credentialsRequest;
        }
    }
}
