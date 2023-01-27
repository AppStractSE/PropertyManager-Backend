using Domain.Domain.Authentication;
using MediatR;

namespace Domain.Features.Commands.Authentication.Create;

public class PostLoginCommand : IRequest<AuthUser>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
