using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queires.DTO;
using SchoolProject.Core.Features.Departments.Queires.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departments.Queires.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
                                       IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdDTO>>
    //IRequestHandler<GetStudentByIdQuery, Response<GetStudentDTO>>,
    // IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListDTO>>

    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                      IDepartmentService departmentService,
                                      IMapper mapper,
                                      IStudentService studentService) : base(stringLocalizer)
        {
            _localizer = stringLocalizer;
            _mapper = mapper;
            _studentService = studentService;
            _departmentService = departmentService;
        }

        #endregion

        #region Handle Functions
        public async Task<Response<GetDepartmentByIdDTO>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var responsse = await _departmentService.GetDepartmentById(request.Id);
            if (responsse == null) return NotFound<GetDepartmentByIdDTO>(_localizer[SharedResourcesKeys.NotFound]);

            var mapper = _mapper.Map<GetDepartmentByIdDTO>(responsse);
            //pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQuerable = _studentService.GetStudentsByDepartmentIDQuerable(request.Id);
            var PaginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            mapper.StudentList = PaginatedList;

            return Success(mapper);








        }
        #endregion

    }
}