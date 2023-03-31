namespace Api.Dto.Request.TeamMember.v1;

public class PutTeamMembersRequestDto
{
    public string TeamId { get; set; }
    public IList<PutTeamMemberRequestDto> TeamMembers { get; set; }
}