using Domain.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Features.Commands.Authentication.Create;

public class PostLoginCommand : IRequest<AuthUser>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
