using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Queires.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public StudentController(IMediator Mediator)
        {
            _Mediator = Mediator;
        }
        [HttpGet(Router.StudentRoute.List)]
        public async Task<IActionResult> GetStudentList()
        {
            var res = await _Mediator.Send(new GetStudentListQuery());
            return Ok(res);

        }
        [HttpGet(Router.StudentRoute.GetByID)]
        public async Task<IActionResult> GetStudentByID([FromRoute] int id)
        {
            var res = await _Mediator.Send(new GetStudentByIdQuery(id));
            return Ok(res);

        }
    }
}
