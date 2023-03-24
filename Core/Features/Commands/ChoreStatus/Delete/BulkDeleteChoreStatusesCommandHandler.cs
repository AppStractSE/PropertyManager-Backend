using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.ChoreStatus;

public class BulkDeleteChoreStatusesCommandHandler : IRequestHandler<BulkDeleteChoreStatusCommand, bool>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public BulkDeleteChoreStatusesCommandHandler(IChoreStatusRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<bool> Handle(BulkDeleteChoreStatusCommand request, CancellationToken cancellationToken)
    {
        var allStatuses = await _repo.GetAllAsync();
        var statusesToDelete = allStatuses.Where(x => request.CustomerChoreId == x.CustomerChoreId.ToString());
        var result = await _repo.DeleteRangeAsync(statusesToDelete);

        if (result)
        {
            statusesToDelete.ToList().ForEach(async x =>
            {
                await _cache.RemoveAsync($"ChoreStatuses:CustomerChore:{request.CustomerChoreId}");
            });
            await _cache.RemoveAsync("ChoreStatuses:");
        }

        return result;
    }
}