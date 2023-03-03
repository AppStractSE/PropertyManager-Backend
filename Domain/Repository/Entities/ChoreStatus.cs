namespace Core.Repository.Entities;

public class ChoreStatus : BaseEntity
{
    public Guid Id { get; set; }
    public string CustomerChoreId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletedDate { get; set; }
    public string DoneBy { get; set; }
}