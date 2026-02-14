using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
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
        private readonly IStringLocalizer<SharedResources> _localizer;

        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentService.Addasync(studentMapper);

            if (result == "Success")
                return Created<string>(_localizer[SharedResourcesKeys.Created]);
            return BadRequest<string>(_localizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(request.Id);
            if (student == null)
                return BadRequest<string>(_localizer[SharedResourcesKeys.NotFound]);

            var studentMapper = _mapper.Map(request, student);
            var result = await _studentService.EditAsync(studentMapper);

            if (result == "Success")
                return Success<string>(_localizer[SharedResourcesKeys.Updated]);
            return BadRequest<string>(_localizer[SharedResourcesKeys.UpdateFailed]);
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(request.Id);
            if (student == null)
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);

            var result = await _studentService.DeleteAsync(student);

            if (result == "Success")
                return Deleted<string>(_localizer[SharedResourcesKeys.Deleted]);
            return BadRequest<string>(_localizer[SharedResourcesKeys.DeletedFailed]);
        }
    }
}
