namespace Domain.Repository.Entities;

public class RoleRule : BaseEntity {
    public Guid Id { get; set; }
    public string RoleId { get; set; }
    public string RuleId { get; set; }
    public bool Enabled { get; set; }
}