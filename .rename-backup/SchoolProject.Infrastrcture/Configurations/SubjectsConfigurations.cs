using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class SubjectsConfigurations : IEntityTypeConfiguration<Subjects>
    {
        public void Configure(EntityTypeBuilder<Subjects> builder)
        {
            // Map Period to SQL int explicitly
            builder.Property(s => s.Period)
                   .HasColumnType("int")
                   .IsRequired(false);

            builder.Property(s => s.SubjectNameAr)
                   .HasMaxLength(500);

            builder.Property(s => s.SubjectNameEn)
                   .HasMaxLength(500);
        }
    }
}

