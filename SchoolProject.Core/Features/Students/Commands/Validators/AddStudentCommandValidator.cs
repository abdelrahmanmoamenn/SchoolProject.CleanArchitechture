using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class AddStudentCommandValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentService;
        public AddStudentCommandValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidtionRules();
            ApplyCustomValidtionRules();
        }

        public void ApplyValidtionRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name Can't be Empty")
                .NotNull().WithMessage("Name can't be null")
                .MaximumLength(10).WithMessage("Max length is 10");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} Can't be Empty")
                .NotNull().WithMessage("{PropertyValue} can't be null")
                .MaximumLength(10).WithMessage("{PropertyName} length is 10");

            RuleFor(x => x.phone)
                .NotEmpty().WithMessage("{PropertyName} Can't be Empty")
                .NotNull().WithMessage("{PropertyValue} can't be null")
                .MaximumLength(10).WithMessage("{PropertyName} length is 10");


        }
        public void ApplyCustomValidtionRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                .WithMessage("Name is Exist");

        }
    }
}
