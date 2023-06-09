namespace Core.Domain;

public class ChoreComment
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string CustomerChoreId { get; set; }
    public string DisplayName { get; set; }
    public string UserId { get; set; }
    public DateTime Time { get; set; }
}