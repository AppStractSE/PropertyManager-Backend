namespace Api.Dto.Request.TeamMember.v1;

public class PostTeamMemberRequestDto
{
    public string UserId { get; set; }
    public string TeamId { get; set; }
    public bool IsTemporary { get; set; }
}