using MediatR;

namespace Domain.Features.Commands.User;

public class AddUserCommand : IRequest<Domain.User>
{
    public string CredId { get; set; }
    public string RoleId { get; set; }
    public string Name { get; set; }
}