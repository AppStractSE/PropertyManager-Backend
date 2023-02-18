using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Teams;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetTeamByIdQueryHandler(ITeamRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Team> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists($"Team:{request.Id}"))
        {
            return await _redisCache.GetAsync<Team>($"Team:{request.Id}");
        }
        
        var mappedDomain = _mapper.Map<Team>(await _repo.GetById(request.Id));
        await _redisCache.SetAsync($"Team:{request.Id}", mappedDomain);
        return mappedDomain;
    }
}