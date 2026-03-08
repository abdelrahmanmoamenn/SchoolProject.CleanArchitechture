using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.IRepositories;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implmentations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        public IDepartmentRepository _departmentRepository { get; set; }
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion
        #region Handle Functions
        public async Task<Department> GetDepartmentById(int id)
        {
            var dept = await _departmentRepository.GetTableNoTracking().Where(x => x.DID.Equals(id))
                                                                        .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                                        .Include(x => x.Instructors)
                                                                        .Include(x => x.Instructor).FirstOrDefaultAsync();
            return dept;
        }

        public async Task<bool> IsDepartmentIdExist(int id)
        {
            return await _departmentRepository.GetTableNoTracking().AnyAsync(x => x.DID.Equals(id));
        }
        #endregion
    }
}
