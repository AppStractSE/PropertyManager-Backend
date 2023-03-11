using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.TeamMember;

public class UpdateTeamMemberCommandHandler : IRequestHandler<UpdateTeamMemberCommand, Domain.TeamMember>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public UpdateTeamMemberCommandHandler(ITeamMemberRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.TeamMember> Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.TeamMember>(request));

        await _redisCache.RemoveAsync("TeamMembers:");
        await _redisCache.RemoveAsync($"User:TeamMembers:{request.UserId}");

        return _mapper.Map<Domain.TeamMember>(response);
    }
}