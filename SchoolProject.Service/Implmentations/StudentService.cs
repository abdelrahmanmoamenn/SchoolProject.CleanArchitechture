using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastrcture.IRepoistories;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implmentations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> GetStudentByIDAsync(int id)
        {
            var student = _studentRepository.GetTableNoTracking()
                .Include(x => x.Department)
                .Where(x => x.StudID.Equals(id))
                .FirstOrDefault();
            return student;
        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentListAsync();

        }
    }
}
