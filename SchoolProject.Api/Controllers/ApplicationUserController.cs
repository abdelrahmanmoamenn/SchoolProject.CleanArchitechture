using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Features.Users.Queires.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize]

    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost(Router.ApplicationUserRoute.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand user)
        {
            var res = await _Mediator.Send(user);
            return NewResult(res);

        }
        [AllowAnonymous]
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
        [HttpPut(Router.ApplicationUserRoute.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand user)
        {
            var res = await _Mediator.Send(user);
            return NewResult(res);

        }
        [HttpPut(Router.ApplicationUserRoute.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand passwordCommand)
        {
            var res = await _Mediator.Send(passwordCommand);
            return NewResult(res);

        }
        [HttpDelete(Router.ApplicationUserRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _Mediator.Send(new DeleteUserCommand(id));
            return NewResult(res);

        }


    }
}
