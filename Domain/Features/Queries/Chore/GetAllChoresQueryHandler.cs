using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Chores;

public class GetAllChoresQueryHandler : IRequestHandler<GetAllChoresQuery, IList<Chore>>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    public GetAllChoresQueryHandler(IChoreRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
    }
    public async Task<IList<Chore>> Handle(GetAllChoresQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("Chores:")) {
            return await _redisCache.GetAsync<IList<Chore>>("Chores:");
        }
        
        var mappedDomain = _mapper.Map<IList<Chore>>(await _repo.GetAllAsync());
        await _redisCache.SetAsync("Chores:", mappedDomain);
        return mappedDomain;
    }
}