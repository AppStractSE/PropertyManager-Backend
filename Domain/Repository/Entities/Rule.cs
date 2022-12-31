namespace Domain.Repository.Entities;

public class Rule : BaseEntity {
    public Guid Id { get; set; }
    public string RuleName { get; set; }

}