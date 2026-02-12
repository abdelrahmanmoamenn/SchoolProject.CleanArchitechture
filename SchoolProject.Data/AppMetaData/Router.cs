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

    }
}
