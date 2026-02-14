using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastrcture.IRepoistories;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implmentations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> Addasync(Student student)
        {

            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";

            }
            catch
            {
                await trans.CommitAsync();
                return "Faild";

            }


        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.Localize(x.NameAr, x.NameEn).Contains(search) || x.Address.Contains(search));
            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudID:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.Localize(x.NameAr, x.NameEn));
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.Department:
                    querable = querable.OrderBy(x => x.Localize(x.Department.DNameAr, x.Department.DNameEn));
                    break;
                default:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
            }
            return querable;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student;
        }

        public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
        {
            var student = _studentRepository.GetTableNoTracking()
                .Include(x => x.Department)
                .Where(x => x.StudID.Equals(id))
                .FirstOrDefault();
            return student;
        }

        public IQueryable<Student> GetStudentQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentListAsync();

        }

        public async Task<bool> IsNameExist(string name)
        {
            var student = _studentRepository.GetTableNoTracking().Where(x => x.Localize(x.NameAr, x.NameEn).Equals(name)).FirstOrDefault();
            if (student == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.Localize(x.NameAr, x.NameEn).Equals(name) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null)
            {
                return false;
            }
            return true;
        }
    }
}
