using MediatR;

namespace Core.Features.Commands.TeamMember;

public class AddTeamMemberCommand : IRequest<Domain.TeamMember>
{
    public string UserId { get; set; }
    public string TeamId { get; set; }
    public bool IsTemporary { get; set; }
}