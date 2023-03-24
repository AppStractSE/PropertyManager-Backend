using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.TeamMember;

public class BulkDeleteTeamMembersCommandHandler : IRequestHandler<BulkDeleteTeamMembersCommand, bool>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public BulkDeleteTeamMembersCommandHandler(ITeamMemberRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<bool> Handle(BulkDeleteTeamMembersCommand request, CancellationToken cancellationToken)
    {
        var teamMembers = await _repo.GetAllAsync();
        var teamMembersToDelete = teamMembers.Where(x => request.TeamId == x.TeamId);
        var result = await _repo.DeleteRangeAsync(teamMembersToDelete);

        if (result)
        {
            teamMembersToDelete.ToList().ForEach(async x =>
            {
                await _cache.RemoveAsync($"TeamMember:{x.UserId}");
            });
            await _cache.RemoveAsync("TeamMembers:");
        }

        return result;
    }
}