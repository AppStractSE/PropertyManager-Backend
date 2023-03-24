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
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    private readonly IMediator _mediator;

    public DeleteCustomerCommandHandler(ICustomerRepository repo, IMediator mediator, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.Customer>(request));

        if (result) 
        {
            await _mediator.Send(new BulkDeleteCustomerChoresCommand { CustomerId = request.Id.ToString() }, cancellationToken); //klar
            await _mediator.Send(new BulkDeleteChoreCommentsCommand { CustomerChoreId = request.Id.ToString() }, cancellationToken);  //klar
            await _mediator.Send(new BulkDeleteChoreStatusCommand { CustomerChoreId = request.Id.ToString() }, cancellationToken); //klar
            await _cache.RemoveAsync("Customers:");
            await _cache.RemoveAsync($"Customer:{request.Id}");

        }

        return result;
    }
}