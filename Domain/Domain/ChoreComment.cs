namespace Domain.Domain;

public class ChoreComment
{
    public Guid Id { get; set; }
    string Message { get; set; }
    string CustomerChoreId { get; set; }
    string UserId { get; set; }
}