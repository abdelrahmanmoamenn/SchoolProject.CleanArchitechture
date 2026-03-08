using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Queires.DTO;
using SchoolProject.Core.Features.Users.Queires.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.Users.Queires.Handlers
{
    public class UserQueryHandler : ResponseHandler,
                            IRequestHandler<GetUserPaginatedListQuery, PaginatedResult<GetUserPaginatedListDTO>>,
                            IRequestHandler<GetUserByIdQuery, Response<GetUserByIdDTO>>
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _localizer = stringLocalizer;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        public async Task<PaginatedResult<GetUserPaginatedListDTO>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedUsers = await _mapper.ProjectTo<GetUserPaginatedListDTO>(users)
                                        .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedUsers;
        }

        public async Task<Response<GetUserByIdDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (user == null)
            {
                return NotFound<GetUserByIdDTO>(_localizer[SharedResourcesKeys.NotFound]);
            }
            var userDto = _mapper.Map<GetUserByIdDTO>(user);
            return Success(userDto);

        }


        #endregion
    }
}
