using MediatR;

namespace Core.Features.Commands.Team;

public class UpdateTeamCommand : IRequest<Domain.Team>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}