using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentMapping()
        {
            CreateMap<EditStudentCommand, Student>()
              .ForMember(dept => dept.DID, opt => opt.MapFrom(src => src.DepartmentId))
              .ForMember(dept => dept.StudID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
