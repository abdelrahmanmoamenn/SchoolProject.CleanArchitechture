using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Requests;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesDto>>
    {
        public int UserId { get; set; }
        public ManageUserRolesQuery(int userId)
        {
            UserId = userId;
        }
    }
}
