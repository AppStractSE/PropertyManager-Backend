using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.TeamMember;

public class UpdateTeamMembersCommandHandler : IRequestHandler<UpdateTeamMembersCommand, IList<Domain.TeamMember>>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    public UpdateTeamMembersCommandHandler(ITeamMemberRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _redisCache = redisCache;
        _mapper = mapper;
    }
    public async Task<IList<Domain.TeamMember>> Handle(UpdateTeamMembersCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateRangeAsync(_mapper.Map<IList<Repository.Entities.TeamMember>>(request.TeamMembers));
        var teamMembers = _mapper.Map<IList<Domain.TeamMember>>(response);
        foreach (var item in teamMembers)
        {
            await _redisCache.RemoveAsync($"User:TeamMembers:{item.UserId}:Team:{item.TeamId}");
        }
        return teamMembers;
    }
}