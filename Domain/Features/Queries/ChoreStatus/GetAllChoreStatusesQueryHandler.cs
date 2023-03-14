using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.ChoreStatuses;

public class GetAllChoreStatuesQueryHandler : IRequestHandler<GetAllChoreStatusesQuery, IList<ChoreStatus>>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetAllChoreStatuesQueryHandler(IChoreStatusRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<ChoreStatus>> Handle(GetAllChoreStatusesQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("ChoreStatuses:"))
        {
            return await _redisCache.GetAsync<IList<ChoreStatus>>("ChoreStatuses:");
        }
        
        var mappedDomain = _mapper.Map<IList<ChoreStatus>>(await _repo.GetAllAsync());
        await _redisCache.SetAsync("ChoreStatuses:", mappedDomain);
        return mappedDomain;
    }
}