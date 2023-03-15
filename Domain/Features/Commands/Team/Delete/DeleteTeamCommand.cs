using MediatR;

namespace Core.Features.Commands.Team;

public class DeleteTeamCommand : IRequest<Domain.Team>
{
    public Guid Id { get; set; }
}