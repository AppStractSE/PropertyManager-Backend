using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.TeamMember;

public class UpdateTeamMemberCommandHandler : IRequestHandler<UpdateTeamMemberCommand, Domain.TeamMember>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public UpdateTeamMemberCommandHandler(ITeamMemberRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.TeamMember> Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.TeamMember>(request));

        await _cache.RemoveAsync("TeamMembers:");
        await _cache.RemoveAsync($"User:TeamMembers:{request.UserId}");

        return _mapper.Map<Domain.TeamMember>(response);
    }
}