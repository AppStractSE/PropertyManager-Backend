using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Repository.Entities;
public class TeamMember : BaseEntity
{
    [ForeignKey("Team")]
    public string UserId { get; set; }
    [ForeignKey("Team")]
    public string TeamId { get; set; }
    public bool IsTemporary { get; set; }
}