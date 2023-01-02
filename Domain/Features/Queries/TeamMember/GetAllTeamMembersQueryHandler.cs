using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetAllTeamMembersQueryHandler : IRequestHandler<GetAllTeamMembersQuery, IList<TeamMember>>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    public GetAllTeamMembersQueryHandler(ITeamMemberRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<TeamMember>> Handle(GetAllTeamMembersQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<TeamMember>>(await _repo.GetAllAsync());
    }
}