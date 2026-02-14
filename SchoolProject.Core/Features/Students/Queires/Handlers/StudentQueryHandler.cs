using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queires.DTO;
using SchoolProject.Core.Features.Students.Queires.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Students.Queires.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
                                       IRequestHandler<GetStudentListQuery, Response<List<GetStudentListDTO>>>,
                                       IRequestHandler<GetStudentByIdQuery, Response<GetStudentDTO>>,
                                       IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListDTO>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<Response<List<GetStudentListDTO>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentsListAsync();
            var studentDTO = _mapper.Map<List<GetStudentListDTO>>(student);
            var res = Success(studentDTO);
            res.Meta = new { count = studentDTO.Count() };
            return res;
        }

        public async Task<Response<GetStudentDTO>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null)
                return NotFound<GetStudentDTO>(_localizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetStudentDTO>(student);
            return Success(result);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListDTO>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListDTO>> expression = e => new GetStudentPaginatedListDTO(e.StudID, e.Localize(e.NameAr, e.NameEn), e.Address, e.Localize(e.Department.DNameAr, e.Department.DNameEn));
            //var querable = _studentService.GetStudentQuerable();
            var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);
            var paginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedList.Meta = new { count = paginatedList.Data.Count() };
            return paginatedList;
        }
    }
}
