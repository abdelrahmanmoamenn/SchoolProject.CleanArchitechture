using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void AddInstructorCommandMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
