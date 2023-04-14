namespace Core.Domain;

public class ArchivedChoreStatus
{
    public Guid Id { get; set; }
    public string CustomerChoreId { get; set; }
    public DateTime CompletedDate { get; set; }
    public string DoneBy { get; set; }
}