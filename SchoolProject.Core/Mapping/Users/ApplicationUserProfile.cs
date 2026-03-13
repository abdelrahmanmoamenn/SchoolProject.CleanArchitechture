using AutoMapper;

namespace SchoolProject.Core.Mapping.Users
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            AddUserMapping();
            GetUserPaginatedListMapping();
            GetUserByIdMapping();
            EditUserMapping();
        }
    }
}
