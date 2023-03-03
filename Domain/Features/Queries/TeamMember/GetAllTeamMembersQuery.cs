using MediatR;

namespace Core.Features.Queries.TeamMembers;

public class GetAllTeamMembersQuery : IRequest<IList<Domain.TeamMember>>
{

}