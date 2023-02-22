using MediatR;

namespace Domain.Features.Commands.TeamMember;

public class UpdateTeamMembersCommand : IRequest<IList<Domain.TeamMember>>
{
    public IList<Domain.TeamMember> TeamMembers { get; set; }
}