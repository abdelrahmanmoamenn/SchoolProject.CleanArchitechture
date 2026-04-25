using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Departments.Queires.DTO;
using SchoolProject.Core.Features.Departments.Queires.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using Serilog;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departments.Queires.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
                                       IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdDTO>>,
                                        IRequestHandler<GetDepartmentStudentListCountQuery, Response<List<GetDepartmentStudentListCountDTO>>>



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
            var response = await _departmentService.GetDepartmentById(request.Id);
            if (response == null) return NotFound<GetDepartmentByIdDTO>(_localizer[SharedResourcesKeys.NotFound]);

            var mapper = _mapper.Map<GetDepartmentByIdDTO>(response);
            //pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQueryable = _studentService.GetStudentsByDepartmentIDQuerable(request.Id);
            var PaginatedList = await studentQueryable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            mapper.StudentList = PaginatedList;
            Log.Information($"Get Department By Id {request.Id}!");

            return Success(mapper);




        }

        public async Task<Response<List<GetDepartmentStudentListCountDTO>>> Handle(GetDepartmentStudentListCountQuery request, CancellationToken cancellationToken)
        {
            var viewDepartmentResult = await _departmentService.GetViewDepartmentDataAsync();
            var result = _mapper.Map<List<GetDepartmentStudentListCountDTO>>(viewDepartmentResult);
            return Success(result);
        }
        #endregion

    }
}