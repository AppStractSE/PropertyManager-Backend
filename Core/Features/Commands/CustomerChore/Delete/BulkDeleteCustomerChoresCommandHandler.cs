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

    public BulkDeleteCustomerChoresCommandHandler(ICustomerChoreRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<bool> Handle(BulkDeleteCustomerChoresCommand request, CancellationToken cancellationToken)
    {
        var chores = await _repo.GetAllAsync();
        var choresToDelete = chores.Where(x => request.CustomerId == x.Id.ToString());
        var result = await _repo.DeleteRangeAsync(choresToDelete);

        if (result)
        {
            await _cache.RemoveAsync("Chores:");
            await _cache.RemoveAsync("CustomerChores:");
        }

        return result;
    }
}