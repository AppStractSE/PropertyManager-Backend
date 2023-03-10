using MediatR;

namespace Domain.Features.Commands.Team;

public class DeleteTeamCommand : IRequest<Domain.Team>
{
    public Guid Id { get; set; }
}