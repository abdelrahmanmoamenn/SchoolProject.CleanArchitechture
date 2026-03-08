using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Queires.DTO;

namespace SchoolProject.Core.Features.Users.Queires.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdDTO>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
