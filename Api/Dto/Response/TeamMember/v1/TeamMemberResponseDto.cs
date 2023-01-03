namespace Api.Dto.Response.TeamMember.v1;
public class TeamMemberResponseDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string TeamId { get; set; }
    public bool IsTemporary { get; set; }
}