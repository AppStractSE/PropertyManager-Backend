using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.TeamMembers;

public class GetAllTeamMembersQueryHandler : IRequestHandler<GetAllTeamMembersQuery, IList<TeamMember>>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetAllTeamMembersQueryHandler(ITeamMemberRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<TeamMember>> Handle(GetAllTeamMembersQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("TeamMembers:"))
        {
            return await _redisCache.GetAsync<IList<TeamMember>>("TeamMembers:");
        }
        
        var mappedDomains = _mapper.Map<IList<TeamMember>>(await _repo.GetAllAsync());

        await _redisCache.SetAsync("TeamMembers:", mappedDomains);
        return mappedDomains;
    }
}