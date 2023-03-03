using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Areas;

public class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, IList<Area>>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    
    public GetAllAreasQueryHandler(IAreaRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Area>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("areas"))
        {
            return await _redisCache.GetAsync<IList<Area>>("areas");
        }

        var response = _mapper.Map<IList<Area>>(await _repo.GetAllAsync());
        await _redisCache.SetAsync("areas", response);
        return response;
    }
}