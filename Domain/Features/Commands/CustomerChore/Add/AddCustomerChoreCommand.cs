using MediatR;

namespace Domain.Features.Commands.CustomerChore;

public class AddCustomerChoreCommand : IRequest<Domain.CustomerChore>
{
    public string CustomerId { get; set; }
    public string ChoreId { get; set; }
    public int Frequency { get; set; }
    public string PeriodicId { get; set; }
}