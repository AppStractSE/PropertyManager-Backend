using Domain.Domain.Authentication;
using Domain.Repository.Entities;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Features.Commands.Authentication.Create;

public class PostLoginCommandHandler : IRequestHandler<PostLoginCommand, AuthUser>
{
    private readonly UserManager<AuthUserEntity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public PostLoginCommandHandler(UserManager<AuthUserEntity> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    public async Task<AuthUser> Handle(PostLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = AuthUtils.GetToken(authClaims, _configuration);

            return new AuthUser()
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
            };
        }
        return null;
    }
}
