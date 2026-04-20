namespace SchoolProject.Data.AppMetaData
{
    public class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string rule = root + "/" + version + "/";
        public const string singleRoute = "{id}";


        public static class StudentRoute
        {


            public const string prefix = rule + "Student/";
            public const string List = prefix + "List/";
            public const string GetByID = prefix + singleRoute;
            public const string Create = prefix + "Create/";
            public const string Edit = prefix + "Edit/";
            public const string Delete = prefix + "Delete/" + singleRoute;
            public const string Paginated = prefix + "Paginated/";




        }
        public static class DepartmentRoute
        {
            public const string Prefix = rule + "Department/";
            public const string GetByID = Prefix + "Id/";
            public const string GetDepartmentStudentsCount = Prefix + "Department-Students-Count/";
            public const string GetDepartmentStudentsCountById = Prefix + "Department-Students-Count-ById/{id}/";
        }

        public static class ApplicationUserRoute
        {
            public const string Prefix = rule + "ApplicationUser/";
            public const string Create = Prefix + "Create/";
            public const string Paginated = Prefix + "Paginated/";
            public const string GetByID = Prefix + singleRoute;
            public const string Edit = Prefix + "Edit/";
            public const string ChangePassword = Prefix + "Change-Password/";
            public const string Delete = Prefix + "Delete/" + singleRoute;
        }

        public static class Authentication
        {
            public const string Prefix = rule + "Authentication/";
            public const string SignIn = Prefix + "SignIn/";
            public const string RefreshToken = Prefix + "Refresh-Token/";
            public const string ValidateToken = Prefix + "Validate-Token/";
            public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";
            public const string SendResetPasswordCode = Prefix + "Send-Reset-Password-Code/";
            public const string ConfirmResetPasswordCode = Prefix + "Confirm-Reset-Password-Code/";
        }
        public static class Authorization
        {
            public const string Prefix = rule + "Authorization/";
            public const string Roles = Prefix + "Roles/";
            public const string Claims = Prefix + "Claims/";

            public const string Create = Roles + "Create/";
            public const string Edit = Roles + "Edit/";
            public const string Delete = Roles + "Delete/" + singleRoute;
            public const string GetByID = Roles + "GetByID/" + singleRoute;
            public const string List = Roles + "List/";
            public const string ManageUserRoles = Roles + "Manage-User-Roles/" + singleRoute;
            public const string UpdateUserRoles = Roles + "Update-User-Roles/";
            public const string ManageUserClaims = Claims + "Manage-User-Claims/" + singleRoute;
            public const string UpdateUserClaims = Claims + "Update-User-Claims/";

        }
        public static class Emails
        {
            public const string Prefix = rule + "Emails/";
            public const string SendEmail = Prefix + "SendEmail/";
        }
    }
}