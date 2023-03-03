using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Periodics;

public class GetAllPeriodicsQueryHandler : IRequestHandler<GetAllPeriodicsQuery, IList<Periodic>>
{
    private readonly IPeriodicRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetAllPeriodicsQueryHandler(IPeriodicRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<IList<Periodic>> Handle(GetAllPeriodicsQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("Periodics:"))
        {
            return await _redisCache.GetAsync<IList<Periodic>>("Periodics:");
        }
        
        var mappedDomain = _mapper.Map<IList<Periodic>>(await _repo.GetAllAsync());
        await _redisCache.SetAsync("Periodics:", mappedDomain);
        return mappedDomain;
    }
}