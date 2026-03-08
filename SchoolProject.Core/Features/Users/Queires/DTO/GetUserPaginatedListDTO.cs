namespace SchoolProject.Core.Features.Users.Queires.DTO
{
    public class GetUserPaginatedListDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
