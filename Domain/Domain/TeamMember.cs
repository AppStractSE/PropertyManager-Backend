namespace Domain.Domain;

public class TeamMember
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string TeamId { get; set; }
    public bool IsTemporary { get; set; }
}