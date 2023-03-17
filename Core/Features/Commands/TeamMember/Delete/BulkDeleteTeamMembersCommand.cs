using MediatR;

namespace Core.Features.Commands.TeamMember;

public class BulkDeleteTeamMembersCommand : IRequest<bool>
{
    public string TeamId { get; set; }
}