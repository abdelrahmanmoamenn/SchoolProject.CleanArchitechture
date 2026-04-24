using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queires.DTO;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
    public class GetDepartmentStudentListCountQuery : IRequest<Response<List<GetDepartmentStudentListCountDTO>>>
    {

    }
}