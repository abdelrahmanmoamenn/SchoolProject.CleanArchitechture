using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastrcture.Data;
using SchoolProject.Infrastrcture.IRepoistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastrcture.Repoistories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentRepository(ApplicationDbContext dbContext)
        {
         _dbContext = dbContext;  
        }
        public async Task<List<Student>> GetStudentListAsync()
        {
            return await _dbContext.students.ToListAsync();
        
        }

     
    }
}
