using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastrcture.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {

        }
        public DbSet<Department> departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<DepartmetSubject> departmetSubjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(
    new Department
    {
        DID = 1,
        DNameEn = "Computer Science",
        DNameAr = "علوم الحاسب"
    }
);


            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudID = 1,
                    NameEn = "Ahmed Mohamed",
                    NameAr = "احمد محمد",
                    Address = "Cairo",
                    Phone = "01012345678",
                    DID = 1
                },
                new Student
                {
                    StudID = 2,
                    NameEn = "Sara Ali",
                    NameAr = "سارة علي",
                    Address = "Giza",
                    Phone = "01198765432",
                    DID = 1
                }
            );
        }

    }
}
