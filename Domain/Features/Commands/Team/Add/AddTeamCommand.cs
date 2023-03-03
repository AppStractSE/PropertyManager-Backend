using MediatR;

namespace Core.Features.Commands.Team;

public class AddTeamCommand : IRequest<Domain.Team>
{
    public string Name { get; set; }
}