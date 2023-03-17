using MediatR;

namespace Core.Features.Commands.Team;

public class DeleteTeamCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}