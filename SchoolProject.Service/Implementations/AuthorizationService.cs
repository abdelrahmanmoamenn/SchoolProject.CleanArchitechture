using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System.Security.Claims;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager,
                                    UserManager<User> userManager,
                                    ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }


        #endregion
        #region handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var userRole = await _roleManager.FindByIdAsync(roleId.ToString());
            if (userRole == null)
                return "NotFound";

            //Check if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(userRole.Name);
            //return exception 
            if (users != null && users.Count() > 0) return "HasUsers";
            var result = await _roleManager.DeleteAsync(userRole);
            if (result.Succeeded)
                return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;

        }

        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var userRole = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (userRole == null)
            {
                return "NotFound";
            }

            userRole.Name = request.Name;
            var result = await _roleManager.UpdateAsync(userRole);
            if (result.Succeeded)
                return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles;
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            return role;

        }

        public async Task<bool> IsRoleExistById(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            return role != null;
        }

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<ManageUserClaimsResult> ManageUserClaimsData(User user)
        {
            var response = new ManageUserClaimsResult();
            var UserClaimsList = new List<UserClaims>();
            var userClaims = await _userManager.GetClaimsAsync(user);
            response.UserId = user.Id;
            foreach (var claim in ClaimsStore.claims)
            {
                var userclaim = new UserClaims();
                userclaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userclaim.Value = true;
                }
                else
                {
                    userclaim.Value = false;
                }
                UserClaimsList.Add(userclaim);
            }
            response.UserClaims = UserClaimsList;
            //return Result
            return response;
        }

        public async Task<ManageUserRolesResult> ManageUserRolesAsync(User user)
        {

            var response = new ManageUserRolesResult();
            var rolesList = new List<UserRoles>();
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userrole = new UserRoles();
                userrole.RoleId = role.Id;
                userrole.RoleName = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userrole.IsSelected = true;
                }
                else
                {
                    userrole.IsSelected = false;
                }
                rolesList.Add(userrole);
            }
            response.Roles = rolesList;
            return response;
        }

        public async Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest request)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                    return "UserIsNull";
                var userClaims = await _userManager.GetClaimsAsync(user);
                var RemovedResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!RemovedResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return "FailedToRemoveOldClaims";
                }
                var selectedClaims = request.UserClaims.Where(c => c.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var AddResult = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!AddResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return "FailedToAddNewClaims";
                }
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "FailedToUpdateUserClaims";
            }
        }

        public async Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                    return "UserIsNull";
                var userRoles = await _userManager.GetRolesAsync(user);
                var RemovedResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!RemovedResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return "FailedToRemoveOldRoles";
                }
                var selectedRoles = request.Roles.Where(r => r.IsSelected == true).Select(r => r.RoleName).ToList();
                var AddResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!AddResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return "FailedToAddNewRoles";
                }
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
            #endregion
        }
    }
}
