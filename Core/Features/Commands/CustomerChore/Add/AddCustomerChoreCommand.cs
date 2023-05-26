using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class AddCustomerChoreCommand : IRequest<Domain.CustomerChore>
{
    public string CustomerId { get; set; }
    public string ChoreId { get; set; }
    public int Frequency { get; set; }
    public string PeriodicId { get; set; }
    public string? Description { get; set; }
}