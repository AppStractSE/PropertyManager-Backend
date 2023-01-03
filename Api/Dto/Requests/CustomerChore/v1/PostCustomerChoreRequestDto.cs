namespace Api.Dto.Request.CustomerChore.v1;

public class PostCustomerChoreRequestDto
{
    public string CustomerId { get; set; }
    public string ChoreId { get; set; }
    public int Frequency { get; set; }
    public string PeriodicId { get; set; }
}