using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.TeamMember;

public class AddTeamMemberCommandHandler : IRequestHandler<AddTeamMemberCommand, Domain.TeamMember>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public AddTeamMemberCommandHandler(ITeamMemberRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.TeamMember> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.TeamMember>(request));
        await _redisCache.RemoveAsync("TeamMembers:");
        await _redisCache.RemoveAsync($"User:TeamMembers:{request.UserId}");
        return _mapper.Map<Domain.TeamMember>(response);
    }
}