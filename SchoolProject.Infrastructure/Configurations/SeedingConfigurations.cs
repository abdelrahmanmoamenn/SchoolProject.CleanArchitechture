using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class SeedingConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // Seed Departments
            builder.HasData(
                new Department
                {
                    DID = 1,
                    DNameAr = "علوم الحاسب",
                    DNameEn = "Computer Science",
                    InsManager = null
                },
                new Department
                {
                    DID = 2,
                    DNameAr = "الهندسة",
                    DNameEn = "Engineering",
                    InsManager = null
                },
                new Department
                {
                    DID = 3,
                    DNameAr = "العلوم",
                    DNameEn = "Science",
                    InsManager = null
                }
            );
        }
    }

    public class SubjectsSeedingConfigurations : IEntityTypeConfiguration<Subjects>
    {
        public void Configure(EntityTypeBuilder<Subjects> builder)
        {
            // Seed Subjects
            builder.HasData(
                new Subjects
                {
                    SubID = 1,
                    SubjectNameAr = "البرمجة بلغة C#",
                    SubjectNameEn = "C# Programming",
                    Period = 60
                },
                new Subjects
                {
                    SubID = 2,
                    SubjectNameAr = "قواعد البيانات",
                    SubjectNameEn = "Databases",
                    Period = 45
                },
                new Subjects
                {
                    SubID = 3,
                    SubjectNameAr = "الويب",
                    SubjectNameEn = "Web Development",
                    Period = 50
                },
                new Subjects
                {
                    SubID = 4,
                    SubjectNameAr = "الفيزياء",
                    SubjectNameEn = "Physics",
                    Period = 40
                },
                new Subjects
                {
                    SubID = 5,
                    SubjectNameAr = "الكيمياء",
                    SubjectNameEn = "Chemistry",
                    Period = 40
                }
            );
        }
    }

    public class InstructorSeedingConfigurations : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            // Seed Instructors
            builder.HasData(
                new Instructor
                {
                    InsId = 1,
                    ENameAr = "أحمد علي",
                    ENameEn = "Ahmed Ali",
                    Address = "Cairo",
                    Position = "Senior Instructor",
                    SupervisorId = null,
                    Salary = 5000M,
                    Image = null,
                    DID = 1
                },
                new Instructor
                {
                    InsId = 2,
                    ENameAr = "فاطمة محمد",
                    ENameEn = "Fatima Muhammad",
                    Address = "Giza",
                    Position = "Instructor",
                    SupervisorId = 1,
                    Salary = 4000M,
                    Image = null,
                    DID = 1
                },
                new Instructor
                {
                    InsId = 3,
                    ENameAr = "محمود حسن",
                    ENameEn = "Mahmoud Hassan",
                    Address = "Alexandria",
                    Position = "Instructor",
                    SupervisorId = 1,
                    Salary = 4000M,
                    Image = null,
                    DID = 2
                },
                new Instructor
                {
                    InsId = 4,
                    ENameAr = "سارة إبراهيم",
                    ENameEn = "Sarah Ibrahim",
                    Address = "Cairo",
                    Position = "Senior Instructor",
                    SupervisorId = null,
                    Salary = 5000M,
                    Image = null,
                    DID = 3
                }
            );
        }
    }

    public class StudentSeedingConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Seed Students
            builder.HasData(
                new Student
                {
                    StudID = 1,
                    NameAr = "علي محمد",
                    NameEn = "Ali Muhammad",
                    Address = "Cairo",
                    Phone = "01012345678",
                    DID = 1
                },
                new Student
                {
                    StudID = 2,
                    NameAr = "سارة أحمد",
                    NameEn = "Sara Ahmed",
                    Address = "Giza",
                    Phone = "01198765432",
                    DID = 1
                },
                new Student
                {
                    StudID = 3,
                    NameAr = "محمود علي",
                    NameEn = "Mahmoud Ali",
                    Address = "Alexandria",
                    Phone = "01556789012",
                    DID = 2
                },
                new Student
                {
                    StudID = 4,
                    NameAr = "فاطمة حسن",
                    NameEn = "Fatima Hassan",
                    Address = "Mansoura",
                    Phone = "01667890123",
                    DID = 3
                },
                new Student
                {
                    StudID = 5,
                    NameAr = "إبراهيم سالم",
                    NameEn = "Ibrahim Salem",
                    Address = "Cairo",
                    Phone = "01778901234",
                    DID = 1
                }
            );
        }
    }

    public class DepartmentSubjectSeedingConfigurations : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {
            // Seed Department Subjects
            builder.HasData(
                new DepartmentSubject { DID = 1, SubID = 1 },
                new DepartmentSubject { DID = 1, SubID = 2 },
                new DepartmentSubject { DID = 1, SubID = 3 },
                new DepartmentSubject { DID = 2, SubID = 1 },
                new DepartmentSubject { DID = 2, SubID = 2 },
                new DepartmentSubject { DID = 3, SubID = 4 },
                new DepartmentSubject { DID = 3, SubID = 5 }
            );
        }
    }

    public class StudentSubjectSeedingConfigurations : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            // Seed Student Subjects with grades
            builder.HasData(
                new StudentSubject { StudID = 1, SubID = 1, grade = 85.5M },
                new StudentSubject { StudID = 1, SubID = 2, grade = 90.0M },
                new StudentSubject { StudID = 2, SubID = 1, grade = 78.0M },
                new StudentSubject { StudID = 2, SubID = 3, grade = 88.5M },
                new StudentSubject { StudID = 3, SubID = 1, grade = 92.0M },
                new StudentSubject { StudID = 3, SubID = 2, grade = 86.0M },
                new StudentSubject { StudID = 4, SubID = 4, grade = 81.0M },
                new StudentSubject { StudID = 4, SubID = 5, grade = 79.5M },
                new StudentSubject { StudID = 5, SubID = 1, grade = 88.0M },
                new StudentSubject { StudID = 5, SubID = 3, grade = 85.0M }
            );
        }
    }

    public class InstructorSubjectSeedingConfigurations : IEntityTypeConfiguration<Ins_Subject>
    {
        public void Configure(EntityTypeBuilder<Ins_Subject> builder)
        {
            // Seed Instructor Subjects
            builder.HasData(
                new Ins_Subject { InsId = 1, SubId = 1 },
                new Ins_Subject { InsId = 1, SubId = 2 },
                new Ins_Subject { InsId = 2, SubId = 3 },
                new Ins_Subject { InsId = 3, SubId = 1 },
                new Ins_Subject { InsId = 4, SubId = 4 },
                new Ins_Subject { InsId = 4, SubId = 5 }
            );
        }
    }
}
