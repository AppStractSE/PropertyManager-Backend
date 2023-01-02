namespace Domain.Domain;

public class RoleRule
{
    public Guid Id { get; set; }
    public string RoleId { get; set; }
    public string RuleId { get; set; }
    public bool Enabled { get; set; }
}