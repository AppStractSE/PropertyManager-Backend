using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class DeleteCustomerChoreCommand : IRequest<Domain.CustomerChore>
{
    public Guid CustomerChoreId { get; set; }
}