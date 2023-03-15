using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class DeleteCustomerChoreCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}