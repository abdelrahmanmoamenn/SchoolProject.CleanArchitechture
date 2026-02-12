using MediatR;
using SchoolProject.Core.Features.Students.Queires.DTO;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Students.Queires.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentPaginatedListDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[]? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
