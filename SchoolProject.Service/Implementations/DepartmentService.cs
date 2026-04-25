using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.IRepositories;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        public IDepartmentRepository _departmentRepository { get; set; }
        private readonly IViewRepository<ViewDepartment> _viewDepartmentRepository;
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository, IViewRepository<ViewDepartment> viewDepartmentRepository)
        {
            _departmentRepository = departmentRepository;
            _viewDepartmentRepository = viewDepartmentRepository;
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

        public async Task<List<ViewDepartment>> GetViewDepartmentDataAsync()
        {
            var viewDepartment = await _viewDepartmentRepository.GetTableNoTracking().ToListAsync();
            return viewDepartment;
        }
        #endregion
    }
}
