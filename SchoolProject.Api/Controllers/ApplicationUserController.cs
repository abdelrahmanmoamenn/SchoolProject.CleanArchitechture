using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Features.Users.Queires.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost(Router.ApplicationUserRoute.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand student)
        {
            var res = await _Mediator.Send(student);
            return NewResult(res);

        }

        [HttpGet(Router.ApplicationUserRoute.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginatedListQuery query)
        {
            var res = await _Mediator.Send(query);
            return Ok(res);

        }

        [HttpGet(Router.ApplicationUserRoute.GetByID)]
        public async Task<IActionResult> GetUserByID([FromRoute] int id)
        {
            var res = await _Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(res);

        }

    }
}
