using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.IRepoistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repoistories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        private readonly DbSet<Student> _students;
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _students = dbContext.Set<Student>();
        }
        public async Task<List<Student>> GetStudentListAsync()
        {
            return await _students.Include(s=>s.Department).ToListAsync();
        
        }

     
    }
}

