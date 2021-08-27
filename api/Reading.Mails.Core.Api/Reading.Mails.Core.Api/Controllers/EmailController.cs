using Microsoft.AspNetCore.Mvc;
using Reading.Mails.Core.Api.Api.Model.Request;
using Reading.Mails.Core.Api.Application.Contracts;
using Reading.Mails.Core.Api.Application.Exceptions;
using Reading.Mails.Core.Api.Domain.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Reading.Mails.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService iEmailService;

        public EmailController(IEmailService iEmailService)
        {
            this.iEmailService = iEmailService;
        }

        [HttpGet, Route("GetEmails", Name = "GetEmails")]
        public async Task<IActionResult> GetEmails(string serverType, string server, 
            int port, string encryption, int index, int items, [FromHeader] string authorization)
        {
            try
            {
                var credentials = GetCredentialsFromHeader(authorization);

                var emailData = new EmailListPetition(serverType, server, port,
                    encryption, credentials.Username, credentials.Password, index, items);

                if (!emailData.IsValidData())
                    return this.BadRequest(emailData.LogErrors);

                var response = await this.iEmailService.GetEmailList(emailData);
                return this.Ok(response);
            }
            catch (NoFoundStrategy ex) 
            {
                return this.BadRequest(new List<string> { ex.Message });
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }


        [HttpGet, Route("GetEmaillBody", Name = "GetEmaillBody")]
        public async Task<IActionResult> GetEmaillBody(string serverType, 
            string server, int port, string encryption, string emailId, [FromHeader] string authorization)
        {
            try
            {
                var credentials = GetCredentialsFromHeader(authorization);
                var emailData = new EmailBodyPetition(serverType, server, port, 
                    encryption, credentials.Username, credentials.Password, emailId);

                if (!emailData.IsValidData())
                    return this.BadRequest(emailData.LogErrors);

                var response = await this.iEmailService.GetEmailBody(emailData);
                return this.Ok(response);
            }
            catch (NoFoundStrategy ex)
            {
                return this.BadRequest(new List<string> { ex.Message });
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        private static CredentialHeaderRequest GetCredentialsFromHeader(string authorization)
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
