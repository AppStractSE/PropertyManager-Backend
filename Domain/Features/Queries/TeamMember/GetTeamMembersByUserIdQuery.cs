using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetTeamMembersByUserIdQuery : IRequest<IList<Domain.TeamMember>>
{
    public string Id { get; set; }
}