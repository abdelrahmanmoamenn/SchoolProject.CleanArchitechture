using MediatR;
using SchoolProject.Core.Features.Users.Queires.DTO;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Users.Queires.Models
{
    public class GetUserPaginatedListQuery : IRequest<PaginatedResult<GetUserPaginatedListDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
