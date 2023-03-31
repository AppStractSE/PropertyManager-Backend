using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Features.Commands.TeamMember;

public class UpdateTeamMembersCommandHandler : IRequestHandler<UpdateTeamMembersCommand, IList<Domain.TeamMember>>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    private readonly ILogger<UpdateTeamMembersCommandHandler> _logger;
    public UpdateTeamMembersCommandHandler(ITeamMemberRepository repo, IMapper mapper, ICache cache, ILogger<UpdateTeamMembersCommandHandler> logger)
    {
        _repo = repo;
        _cache = cache;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IList<Domain.TeamMember>> Handle(UpdateTeamMembersCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateRangeAsync(_mapper.Map<IList<Repository.Entities.TeamMember>>(request.TeamMembers));
        var teamMembers = _mapper.Map<IList<Domain.TeamMember>>(response);

        await _cache.RemoveAsync("TeamMembers:");
        foreach (var item in teamMembers)
        {
            await _cache.RemoveAsync($"User:TeamMembers:{item.UserId}");
        }
        return teamMembers;
    }
}