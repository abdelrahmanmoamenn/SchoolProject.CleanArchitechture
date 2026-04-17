using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<bool> IsRoleExistById(int roleId);
        public Task<List<Role>> GetAllRolesAsync();
        public Task<Role> GetRoleByIdAsync(int roleId);
        public Task<ManageUserRolesDto> ManageUserRolesAsync(User user);
        public Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request);


    }
}
