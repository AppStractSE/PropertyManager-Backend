using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetAllTeamMembersQuery : IRequest<IList<Domain.TeamMember>>
{

}