using MediatR;

namespace Core.Features.Commands.TeamMember;

public class UpdateTeamMemberCommand : IRequest<Domain.TeamMember>
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string TeamId { get; set; }
    public bool IsTemporary { get; set; }
}