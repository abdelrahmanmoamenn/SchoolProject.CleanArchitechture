using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{

    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpPost(Router.InstructorRoute.Create)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddInstructor([FromForm] AddInstructorCommand command)
        {
            return NewResult(await _Mediator.Send(command));
        }
    }
}
