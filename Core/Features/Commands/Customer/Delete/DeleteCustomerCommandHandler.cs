using Core.Features.Commands.ChoreComment;
using Core.Features.Commands.ChoreStatus;
using Core.Features.Commands.CustomerChore;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Customer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly ICustomerRepository _repo;
    private readonly ICache _cache;
    private readonly IMediator _mediator;
    
    public DeleteCustomerCommandHandler(ICustomerRepository repo, IMediator mediator, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mediator = mediator;
    }
    
    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repo.GetById(request.CustomerId.ToString());
        var result = await _repo.DeleteAsync(customer);

        if (result) 
        {
            await _mediator.Send(new BulkDeleteCustomerChoresCommand { CustomerId = request.CustomerId.ToString() }, cancellationToken);
            await _cache.RemoveAsync("Customers:");
            await _cache.RemoveAsync($"Customer:{request.CustomerId}");
        }

        return result;
    }
}