using Domain.Domain.Authentication;
using Domain.Features.Commands.Authentication.Create;
using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Domain.Features.Commands.Area;

public class PostRegisterCommandHandler : IRequestHandler<PostRegisterCommand, IdentityResult>
{
    private readonly UserManager<AuthUserEntity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IRedisCache _redisCache;

    public PostRegisterCommandHandler(UserManager<AuthUserEntity> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<IdentityResult> Handle(PostRegisterCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByNameAsync(request.Username);
        if (userExists != null)
        {
            return null;
        }

        AuthUserEntity user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username,
            DisplayName = request.DisplayName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded || result == null)
        {
            return result;
        }
        else
        {
            await _redisCache.RemoveAsync("AllUsers:");
            await InitRoles();
            return await AddRole(request.Role, user);
        }
    }

    private async Task<IdentityResult> AddRole(string role, AuthUserEntity user)
    {
        if (await _roleManager.RoleExistsAsync(role))
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
        throw new Exception("Role does not exist, created user cannot be added to a role.");
    }

    private async Task InitRoles()
    {
        if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
        }
        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }
        if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
        }

    }
}