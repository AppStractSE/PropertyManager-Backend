using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetTeamMembersByUserIdQueryHandler : IRequestHandler<GetTeamMembersByUserIdQuery, IList<TeamMember>>
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetTeamMembersByUserIdQueryHandler(ITeamMemberRepository teamMemberRepository, IMapper mapper, IMediator mediator, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _teamMemberRepository = teamMemberRepository;
        _mapper = mapper;
    }

    public async Task<IList<Domain.TeamMember>> Handle(GetTeamMembersByUserIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists($"User:TeamMembers:{ request.Id}"))
        {
            return await _redisCache.GetAsync<IList<Domain.TeamMember>>($"User:TeamMembers:{request.Id}");
        }
        
        var teamMembers = _mapper.Map<IList<Domain.TeamMember>>(await _teamMemberRepository.GetQuery(x => x.UserId == request.Id));
        await _redisCache.SetAsync($"User:TeamMembers:{request.Id}", teamMembers);
        return teamMembers;
    }
}