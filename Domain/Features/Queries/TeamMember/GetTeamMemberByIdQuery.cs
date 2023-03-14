using MediatR;

namespace Core.Features.Queries.TeamMembers;

public class GetTeamMemberByIdQuery : IRequest<Domain.TeamMember>
{
    public string Id { get; set; }
}