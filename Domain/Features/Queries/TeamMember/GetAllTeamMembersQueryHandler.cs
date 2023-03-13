using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetAllTeamMembersQueryHandler : IRequestHandler<GetAllTeamMembersQuery, IList<TeamMember>>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAllTeamMembersQueryHandler(ITeamMemberRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<TeamMember>> Handle(GetAllTeamMembersQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("TeamMembers:"))
        {
            return await _cache.GetAsync<IList<TeamMember>>("TeamMembers:");
        }

        var mappedDomains = _mapper.Map<IList<TeamMember>>(await _repo.GetAllAsync());

        await _cache.SetAsync("TeamMembers:", mappedDomains);
        return mappedDomains;
    }
}