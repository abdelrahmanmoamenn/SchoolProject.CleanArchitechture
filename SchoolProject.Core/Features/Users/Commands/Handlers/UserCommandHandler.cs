using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
                                      IRequestHandler<AddUserCommand, Response<string>>,
                                      IRequestHandler<EditUserCommand, Response<string>>,
                                      IRequestHandler<DeleteUserCommand, Response<string>>,
                                      IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserCommandHandler> _logger;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper, UserManager<User> userManager, IApplicationUserService applicationUserService,
                                  ILogger<UserCommandHandler> logger) : base(stringLocalizer)
        {
            _mapper = mapper;
            _localizer = stringLocalizer;
            _userManager = userManager;
            _logger = logger;
            _applicationUserService = applicationUserService;
        }

        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<User>(request);
            //Create
            var createResult = await _applicationUserService.AddUserAsync(identityUser, request.Password);
            switch (createResult)
            {
                case "EmailIsExist": return BadRequest<string>(_localizer[SharedResourcesKeys.EmailIsExist]);
                case "UserNameIsExist": return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);
                case "ErrorInCreateUser": return BadRequest<string>(_localizer[SharedResourcesKeys.FaildToAddUser]);
                case "Failed": return BadRequest<string>(_localizer[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(createResult);
            }
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var OldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (OldUser == null)
            {
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
            }
            var NewUser = _mapper.Map(request, OldUser);

            var userName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == NewUser.UserName && x.Id != NewUser.Id);
            if (userName != null)
            {
                return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);
            }

            var result = await _userManager.UpdateAsync(NewUser);
            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return Success<string>(_localizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return Success<string>(_localizer[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
            }
            if (request.CurrentPassword == request.NewPassword)
            {
                return BadRequest<string>(_localizer[SharedResourcesKeys.NewPasswordSameAsCurrent]);
            }
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return Success<string>(_localizer[SharedResourcesKeys.Updated]);
        }
        #endregion
    }
}