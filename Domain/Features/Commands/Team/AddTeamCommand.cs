using MediatR;

namespace Domain.Features.Commands.Team;

public class AddTeamCommand : IRequest<Domain.Team>
{
    public string Name  { get; set; }
}