using MediatR;

namespace Domain.Features.Commands.Team;

public class UpdateTeamCommand : IRequest<Domain.Team>
{
    public Guid Id { get; set; }
    public string Name  { get; set; }
}