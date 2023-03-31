using MediatR;

namespace Core.Features.Commands.TeamMember;

public class UpdateTeamMembersCommand : IRequest<IList<Domain.TeamMember>>
{
    public string TeamId { get; set; }
    public IList<Domain.TeamMember> TeamMembers { get; set; }
}