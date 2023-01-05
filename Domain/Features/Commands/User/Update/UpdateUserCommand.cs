using MediatR;

namespace Domain.Features.Commands.User;

public class UpdateUserCommand : IRequest<Domain.User>
{
    public Guid Id { get; set; }
    public string CredId { get; set; }
    public string RoleId { get; set; }
    public string Name { get; set; }
}