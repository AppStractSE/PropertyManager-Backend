using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Teams;

public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, IList<Team>>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetAllTeamsQueryHandler(ITeamRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Team>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("Teams:"))
        {
            return await _redisCache.GetAsync<IList<Team>>("Teams:");
        }
        
        var mappedDomain = _mapper.Map<IList<Team>>(await _repo.GetAllAsync());
        await _redisCache.SetAsync("Teams:", mappedDomain);
        return mappedDomain;
    }
}