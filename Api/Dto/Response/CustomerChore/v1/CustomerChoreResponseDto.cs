namespace Api.Dto.Response.CustomerChore.v1;

public class CustomerChoreResponseDto
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; }
    public string ChoreId { get; set; }
    public int Frequency { get; set; }
    public string PeriodicId { get; set; }
    public Core.Domain.Periodic Periodic { get; set; }
    public Core.Domain.Chore Chore { get; set; }
    public Core.Domain.Customer Customer { get; set; }
}