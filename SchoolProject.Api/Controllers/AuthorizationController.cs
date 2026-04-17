using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.Authorization.Create)]
        public async Task<IActionResult> Create([FromBody] AddRoleCommand user)
        {
            var res = await _Mediator.Send(user);
            return NewResult(res);

        }
        [HttpPost(Router.Authorization.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditRoleCommand user)
        {
            var res = await _Mediator.Send(user);
            return NewResult(res);

        }
        [HttpDelete(Router.Authorization.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(res);

        }
        [HttpGet(Router.Authorization.GetByID)]
        public async Task<IActionResult> GetRoleByID([FromRoute] int id)
        {
            var res = await _Mediator.Send(new GetRoleByIdQuery(id));
            return NewResult(res);

        }
        [HttpGet(Router.Authorization.List)]
        public async Task<IActionResult> GetRolesList()
        {
            var res = await _Mediator.Send(new GetRolesListQuery());
            return NewResult(res);

        }
        [HttpGet(Router.Authorization.ManageUserRoles)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int id)
        {
            var res = await _Mediator.Send(new ManageUserRolesQuery(id));
            return NewResult(res);

        }
        [HttpPost(Router.Authorization.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand user)
        {
            var res = await _Mediator.Send(user);
            return NewResult(res);

        }
    }
}
