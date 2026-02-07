using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queires.DTO;
using SchoolProject.Core.Features.Students.Queires.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queires.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
                                       IRequestHandler<GetStudentListQuery, Response<List<GetStudentListDTO>>>,
                                       IRequestHandler<GetStudentByIdQuery, Response<GetStudentDTO>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentQueryHandler(IStudentService studentService , IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task <Response<List<GetStudentListDTO>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var student= await _studentService.GetStudentsListAsync();
            var studentDTO = _mapper.Map<List<GetStudentListDTO>>(student);
            return Success(studentDTO);
        }

        public async Task<Response<GetStudentDTO>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIDAsync(request.Id);
            if (student == null) return NotFound<GetStudentDTO>("Object Not Found");
            var result = _mapper.Map<GetStudentDTO>(student);
            return Success(result);
        }
    }
}
