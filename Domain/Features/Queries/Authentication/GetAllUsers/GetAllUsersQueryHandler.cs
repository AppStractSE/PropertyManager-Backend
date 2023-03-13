using Domain.Domain.Authentication;
using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Domain.Features.Queries.Authentication.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IList<User>>
{
    private readonly UserManager<AuthUserEntity> _userManager;
    private readonly ICache _cache;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(UserManager<AuthUserEntity> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, ICache cache)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IList<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("AllUsers:"))
        {
            return await _cache.GetAsync<IList<User>>("AllUsers:");
        }

        var users = await _userManager.GetUsersInRoleAsync(UserRoles.User);

        var mappedUsers = users.Select(x => new User()
        {
            UserId = x.Id,
            DisplayName = x.DisplayName
        }).ToList();

        await _cache.SetAsync("AllUsers:", mappedUsers);
        return mappedUsers;
    }
}
