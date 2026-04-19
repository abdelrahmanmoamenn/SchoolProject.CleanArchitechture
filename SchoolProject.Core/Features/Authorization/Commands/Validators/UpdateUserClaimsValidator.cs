using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class UpdateUserClaimsValidator : AbstractValidator<UpdateUserClaimsCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region Constructors

        #endregion
        public UpdateUserClaimsValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.UserClaims)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}