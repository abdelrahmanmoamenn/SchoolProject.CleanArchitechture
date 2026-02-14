using SchoolProject.Core.Features.Students.Queires.DTO;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListDTO>()
              .ForMember(dept => dept.Department, opt => opt.MapFrom(src => src.Localize(src.Department.DNameAr, src.Department.DNameEn)))
              .ForMember(dept => dept.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
            ;
        }
    }
}
