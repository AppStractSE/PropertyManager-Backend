namespace Api.Dto.Request.TeamMember.v1;

public class PutTeamMemberRequestDto
{
    public string UserId { get; set; }
    public string TeamId { get; set; }
    public bool IsTemporary { get; set; }
}






