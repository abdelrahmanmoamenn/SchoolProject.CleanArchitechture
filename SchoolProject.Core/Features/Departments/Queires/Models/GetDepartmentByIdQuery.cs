using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queires.DTO;

namespace SchoolProject.Core.Features.Departments.Queires.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdDTO>>
    {
        public int Id { get; set; }
        public int StudentPageSize { get; set; }
        public int StudentPageNumber { get; set; }

    }
}
