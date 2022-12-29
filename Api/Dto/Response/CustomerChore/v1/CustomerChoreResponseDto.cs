namespace Api.Dto.Response.CustomerChore.v1;

public class CustomerChoreResponseDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ChoreId { get; set; }
    public int Frequency { get; set; }
    public Guid PeriodicId { get; set; }
}