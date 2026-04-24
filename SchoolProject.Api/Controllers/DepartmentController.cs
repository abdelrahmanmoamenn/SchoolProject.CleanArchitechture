using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Departments.Queires.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{

    [ApiController]
    [Authorize]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRoute.GetByID)]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            var res = await _Mediator.Send(query);
            return NewResult(res);

        }
        [HttpGet(Router.DepartmentRoute.GetDepartmentStudentsCount)]
        public async Task<IActionResult> GetDepartmentStudentsCount()
        {
            return NewResult(await _Mediator.Send(new GetDepartmentStudentListCountQuery()));
        }
    }
}
