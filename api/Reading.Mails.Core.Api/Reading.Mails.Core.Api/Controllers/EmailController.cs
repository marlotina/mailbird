using Microsoft.AspNetCore.Mvc;
using Reading.Mails.Core.Api.Application.Contracts;
using Reading.Mails.Core.Api.Application.Exceptions;
using Reading.Mails.Core.Api.Domain.Model;
using System;
using System.Collections.Generic;
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
            int port, string encryption, string username, string password, int index, int items)
        {
            try
            {
                var emailData = new EmailListPetition(serverType, server, port,
                    encryption, username, password, index, items);

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
            string server, int port, string encryption, string username, string password, int emailId)
        {
            try
            {
                var emailData = new EmailBodyPetition(serverType, server, port, 
                    encryption, username, password, emailId);

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
    }
}
