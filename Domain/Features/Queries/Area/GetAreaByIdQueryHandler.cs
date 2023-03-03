using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Areas;

public class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, Area>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    
    public GetAreaByIdQueryHandler(IAreaRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
    }
    public async Task<Area> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists($"Area:{request.Id}"))
        {
            return await _redisCache.GetAsync<Area>($"area_{request.Id}");
        }

        var area = _mapper.Map<Area>(await _repo.GetById(request.Id));
        await _redisCache.SetAsync($"Area:{area.Id}", area);
        return area;
    }
}