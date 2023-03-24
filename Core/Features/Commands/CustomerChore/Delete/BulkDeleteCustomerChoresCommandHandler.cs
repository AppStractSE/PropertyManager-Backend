using Core.Features.Commands.ChoreStatus;
using Core.Features.Commands.CustomerChore;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.ChoreComment;

public class BulkDeleteCustomerChoresCommandHandler : IRequestHandler<BulkDeleteCustomerChoresCommand, bool>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    private readonly IMediator _mediator;

    public BulkDeleteCustomerChoresCommandHandler(ICustomerChoreRepository repo, IMapper mapper, ICache cache, IMediator mediator)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<bool> Handle(BulkDeleteCustomerChoresCommand request, CancellationToken cancellationToken)
    {
        var chores = await _repo.GetAllAsync();
        var choresToDelete = chores.Where(x => request.CustomerId == x.CustomerId);
        var choreIds = choresToDelete.Select(x => x.Id.ToString());
        
        var result = await _repo.DeleteRangeAsync(choresToDelete);

        if (result)
        {
            foreach (var choreId in choreIds)
            {
                await _mediator.Send(new BulkDeleteChoreCommentsCommand { CustomerChoreId = choreId }, cancellationToken);
                await _mediator.Send(new BulkDeleteChoreStatusCommand { CustomerChoreId = choreId }, cancellationToken);
            }
            await _cache.RemoveAsync("Chores:"); //Osäker på om denna cache verkligen behöver tas bort.
            await _cache.RemoveAsync("CustomerChores:");
        }

        return result;
    }
}