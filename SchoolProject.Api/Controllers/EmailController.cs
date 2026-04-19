using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class EmailController : AppControllerBase
    {
        [HttpPost(Router.Emails.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand emailCommand)
        {
            var res = await _Mediator.Send(emailCommand);
            return NewResult(res);

        }
    }
}
