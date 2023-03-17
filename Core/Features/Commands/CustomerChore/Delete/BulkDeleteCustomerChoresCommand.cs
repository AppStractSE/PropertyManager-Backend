using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class BulkDeleteCustomerChoresCommand : IRequest<bool>
{
    public string CustomerId { get; set; }
}