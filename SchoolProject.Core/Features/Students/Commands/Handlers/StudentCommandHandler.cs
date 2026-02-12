using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
                                        IRequestHandler<AddStudentCommand, Response<string>>,
                                        IRequestHandler<EditStudentCommand, Response<string>>,
                                        IRequestHandler<DeleteStudentCommand, Response<string>>


    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;


        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentService.Addasync(studentMapper);

            if (result == "Success") return Created("Added Successfully");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(request.Id);
            if (student == null) return BadRequest<string>();


            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentService.EditAsync(studentMapper);

            if (result == "Success") return Success($"Edit {studentMapper.StudID} Successfully");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(request.Id);
            if (student == null) return NotFound<string>("Student doesnot Exist");

            var result = await _studentService.DeleteAsync(student);

            if (result == "Success") return Deleted<string>($"Deleted {student.StudID} Successfully");
            return BadRequest<string>();
        }
    }
}
