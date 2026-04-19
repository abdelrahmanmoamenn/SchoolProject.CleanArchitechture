using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.DTO;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
                                    IRequestHandler<GetRolesListQuery, Response<List<GetRolesListDto>>>,
                                    IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdDto>>,
                                    IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>>


    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructors
        public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                IAuthorizationService authorizationService,
                                IMapper mapper,
                                UserManager<User> userManager) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetRolesListDto>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetAllRolesAsync();
            var mappedRoles = _mapper.Map<List<GetRolesListDto>>(roles);
            return Success(mappedRoles);
        }

        public async Task<Response<GetRoleByIdDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null)
                return NotFound<GetRoleByIdDto>(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
            var mappedRole = _mapper.Map<GetRoleByIdDto>(role);
            return Success(mappedRole);
        }

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound<ManageUserRolesResult>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            var userRoles = await _authorizationService.ManageUserRolesAsync(user);
            return Success(userRoles);
        }
        #endregion
    }
}
