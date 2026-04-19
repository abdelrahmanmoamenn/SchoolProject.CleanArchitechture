namespace SchoolProject.Data.Results
{
    public class ManageUserClaimsResult
    {
        public int UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }
    public class UserClaims
    {
        public bool Value { get; set; }
        public string Type { get; set; }
    }
}
