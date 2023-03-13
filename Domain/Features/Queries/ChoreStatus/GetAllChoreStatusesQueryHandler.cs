using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.ChoreStatuses;

public class GetAllChoreStatuesQueryHandler : IRequestHandler<GetAllChoreStatusesQuery, IList<ChoreStatus>>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAllChoreStatuesQueryHandler(IChoreStatusRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<ChoreStatus>> Handle(GetAllChoreStatusesQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("ChoreStatuses:"))
        {
            return await _cache.GetAsync<IList<ChoreStatus>>("ChoreStatuses:");
        }

        var mappedDomain = _mapper.Map<IList<ChoreStatus>>(await _repo.GetAllAsync());
        await _cache.SetAsync("ChoreStatuses:", mappedDomain);
        return mappedDomain;
    }
}