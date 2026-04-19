using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queires.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class StudentController : AppControllerBase
    {

        [HttpGet(Router.StudentRoute.List)]
        public async Task<IActionResult> GetStudentList()
        {
            var res = await _Mediator.Send(new GetStudentListQuery());
            return Ok(res);

        }
        [HttpGet(Router.StudentRoute.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            var res = await _Mediator.Send(query);
            return Ok(res);

        }
        [HttpGet(Router.StudentRoute.GetByID)]
        public async Task<IActionResult> GetStudentByID([FromRoute] int id)
        {
            var res = await _Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(res);

        }
        [Authorize(Policy = "CreateStudent")]
        [HttpPost(Router.StudentRoute.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand student)
        {
            var res = await _Mediator.Send(student);
            return NewResult(res);

        }
        [Authorize(Policy = "EditStudent")]

        [HttpPut(Router.StudentRoute.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand student)
        {
            var res = await _Mediator.Send(student);
            return NewResult(res);

        }
        [Authorize(Policy = "DeleteStudent")]

        [HttpDelete(Router.StudentRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(res);

        }
    }
}
