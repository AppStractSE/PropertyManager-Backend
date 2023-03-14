using MediatR;

namespace Core.Features.Queries.TeamMembers;

public class GetTeamMembersByUserIdQuery : IRequest<IList<Domain.TeamMember>>
{
    public string Id { get; set; }
}