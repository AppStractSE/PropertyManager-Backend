namespace Core.Repository.Entities;

public class ChoreComment : BaseEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string CustomerChoreId { get; set; }
    public string DisplayName { get; set; }
    public DateTime Time { get; set; }
}