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
            public const string Delete = prefix + singleRoute;
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
            public const string Delete = Prefix + singleRoute;
        }
    }
}