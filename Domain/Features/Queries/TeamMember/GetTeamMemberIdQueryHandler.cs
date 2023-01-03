using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetTeamMemberByIdQueryHandler : IRequestHandler<GetTeamMemberByIdQuery, TeamMember>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    public GetTeamMemberByIdQueryHandler(ITeamMemberRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<TeamMember> Handle(GetTeamMemberByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<TeamMember>(await _repo.GetById(request.Id));
    }
}