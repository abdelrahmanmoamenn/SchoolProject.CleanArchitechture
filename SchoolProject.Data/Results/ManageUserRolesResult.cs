namespace SchoolProject.Data.Results
{
    public class ManageUserRolesResult
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<UserRoles> Roles { get; set; }
    }
    public class UserRoles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
