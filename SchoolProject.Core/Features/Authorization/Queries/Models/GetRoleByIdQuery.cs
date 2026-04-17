using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.DTO;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdDto>>
    {
        public int Id { get; set; }
        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }

    }
}
