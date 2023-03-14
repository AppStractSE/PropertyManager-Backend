using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.TeamMembers;

public class GetTeamMemberByIdQueryHandler : IRequestHandler<GetTeamMemberByIdQuery, TeamMember>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetTeamMemberByIdQueryHandler(ITeamMemberRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<TeamMember> Handle(GetTeamMemberByIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists($"TeamMember:{request.Id}"))
        {
            return await _redisCache.GetAsync<TeamMember>($"TeamMember:{request.Id}");
        }
        
        var mappedDomain = _mapper.Map<TeamMember>(await _repo.GetById(request.Id));
        await _redisCache.SetAsync($"TeamMember:{request.Id}", mappedDomain);
        return mappedDomain;
    }
}