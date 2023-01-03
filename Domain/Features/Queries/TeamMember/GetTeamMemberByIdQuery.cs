using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetTeamMemberByIdQuery : IRequest<Domain.TeamMember>
{
    public string Id { get; set; }
}