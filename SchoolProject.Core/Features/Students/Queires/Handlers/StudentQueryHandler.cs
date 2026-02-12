using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queires.DTO;
using SchoolProject.Core.Features.Students.Queires.Models;
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

        public StudentQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task<Response<List<GetStudentListDTO>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentsListAsync();
            var studentDTO = _mapper.Map<List<GetStudentListDTO>>(student);
            return Success(studentDTO);
        }

        public async Task<Response<GetStudentDTO>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null) return NotFound<GetStudentDTO>("Object Not Found");
            var result = _mapper.Map<GetStudentDTO>(student);
            return Success(result);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListDTO>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListDTO>> expression = e => new GetStudentPaginatedListDTO(e.StudID, e.Name, e.Address, e.Department.DName);
            //var querable = _studentService.GetStudentQuerable();
            var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);
            var paginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}
