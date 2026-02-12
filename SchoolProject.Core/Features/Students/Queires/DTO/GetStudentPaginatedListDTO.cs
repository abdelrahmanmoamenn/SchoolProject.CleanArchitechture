namespace SchoolProject.Core.Features.Students.Queires.DTO
{
    public class GetStudentPaginatedListDTO
    {
        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Department { get; set; }
        public GetStudentPaginatedListDTO(int _StudID, string _Name, string _Address, string _Department)
        {
            StudID = _StudID;
            Name = _Name;
            Address = _Address;
            Department = _Department;

        }
    }
}
