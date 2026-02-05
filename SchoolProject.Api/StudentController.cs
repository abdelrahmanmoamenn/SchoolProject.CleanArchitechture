using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Queires.Models;

namespace SchoolProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public StudentController(IMediator Mediator)
        {
            _Mediator = Mediator;
        }
        [HttpGet("/student/List")]
        public async Task<IActionResult> GetStudentList()
        {
            var res = await _Mediator.Send(new GetStudentListQuery());
            return Ok(res);

        }
    }
}
