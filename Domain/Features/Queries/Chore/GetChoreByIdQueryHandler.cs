using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Chores;

public class GetChoreByIdQueryHandler : IRequestHandler<GetChoreByIdQuery, Chore>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    
    public GetChoreByIdQueryHandler(IChoreRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
    }
    public async Task<Chore> Handle(GetChoreByIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists($"Chore:{request.Id}"))
        {
            return await _redisCache.GetAsync<Chore>($"Chore:{request.Id}");
        }
        
        var mappedDomain = _mapper.Map<Chore>(await _repo.GetById(request.Id));
        await _redisCache.SetAsync($"Chore:{request.Id}", mappedDomain);
        return mappedDomain;
    }
}