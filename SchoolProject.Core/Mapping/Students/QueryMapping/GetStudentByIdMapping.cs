using SchoolProject.Core.Features.Students.Queires.DTO;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<Student, GetStudentDTO>()
              .ForMember(dept => dept.Department, opt => opt.MapFrom(src => src.Department.DName));
        }

    }
}
