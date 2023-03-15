using MediatR;

namespace Core.Features.Commands.TeamMember;

public class AddTeamMembersCommand : IRequest<IList<Domain.TeamMember>>
{
    public IList<Domain.TeamMember> TeamMembers { get; set; }
}