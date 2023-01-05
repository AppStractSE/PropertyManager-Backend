using Domain.Domain.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Features.Commands.Authentication.Create
{
    public class PostRegisterCommand : IRequest<IdentityResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = UserRoles.User;
    }
}
