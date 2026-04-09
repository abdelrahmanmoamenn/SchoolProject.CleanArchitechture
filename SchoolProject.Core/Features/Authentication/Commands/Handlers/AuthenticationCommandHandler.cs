using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
                                                 IRequestHandler<SignInCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthenticationCommandHandler> _logger;


        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                            SignInManager<User> signInManager,
                                            IAuthenticationService authenticationService,
                                            UserManager<User> userManager,
                                            ILogger<AuthenticationCommandHandler> logger) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _logger = logger;
        }




        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sign-in attempt for user: {UserName}", request.UserName);

            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            //Return The UserName Not Found
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserName}", request.UserName);
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);
            }

            _logger.LogInformation("User found: {UserId}, Email: {Email}, EmailConfirmed: {EmailConfirmed}", user.Id, user.Email, user.EmailConfirmed);

            //try to sign in
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //if Failed Return Password is wrong
            if (!signInResult.Succeeded)
            {
                _logger.LogWarning("Password verification failed for user: {UserName}. IsLockedOut: {IsLockedOut}, IsNotAllowed: {IsNotAllowed}, RequiresTwoFactor: {RequiresTwoFactor}",
                    request.UserName, signInResult.IsLockedOut, signInResult.IsNotAllowed, signInResult.RequiresTwoFactor);
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);
            }

            //confirm email
            if (!user.EmailConfirmed)
            {
                _logger.LogWarning("Email not confirmed for user: {UserName}", request.UserName);
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);
            }

            //Generate Token
            _logger.LogInformation("Generating JWT token for user: {UserName}", request.UserName);
            var result = await _authenticationService.GetJWTToken(user);

            //return Token 
            _logger.LogInformation("Sign-in successful for user: {UserName}", request.UserName);
            return Success(result);

        }
        #endregion

    }
}
