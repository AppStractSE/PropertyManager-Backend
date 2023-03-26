namespace Api.Dto.Request.CustomerChore.v1;

public class PutCustomerChoreRequestDto
{
    public Guid Id { get; set; }
    public int Frequency { get; set; }
    public string PeriodicId { get; set; }
}



