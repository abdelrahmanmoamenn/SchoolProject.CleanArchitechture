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
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper, UserManager<User> userManager,
                                  ILogger<UserCommandHandler> logger) : base(stringLocalizer)
        {
            _mapper = mapper;
            _localizer = stringLocalizer;
            _userManager = userManager;
            _logger = logger;
        }

        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting user creation for: {UserName}", request.UserName);

                var userEmail = await _userManager.FindByEmailAsync(request.Email);
                if (userEmail != null)
                {
                    _logger.LogWarning("Email already exists: {Email}", request.Email);
                    return BadRequest<string>(_localizer[SharedResourcesKeys.EmailIsExist]);
                }

                var userName = await _userManager.FindByNameAsync(request.UserName);
                if (userName != null)
                {
                    _logger.LogWarning("Username already exists: {UserName}", request.UserName);
                    return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);
                }

                _logger.LogInformation("Mapping AddUserCommand to User entity");
                var user = _mapper.Map<User>(request);

                _logger.LogInformation("User mapped successfully. Properties: FullName={FullName}, UserName={UserName}, Email={Email}, EmailConfirmed={EmailConfirmed}",
                    user.FullName, user.UserName, user.Email, user.EmailConfirmed);

                user.EmailConfirmed = true;

                _logger.LogInformation("Creating user with UserManager");
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("User creation failed: {Errors}", errors);
                    return BadRequest<string>(errors);
                }

                _logger.LogInformation("User created successfully: {UserName}", request.UserName);
                return Created("");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during user creation: {Message}", ex.Message);
                return BadRequest<string>($"Error: {ex.Message}");
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