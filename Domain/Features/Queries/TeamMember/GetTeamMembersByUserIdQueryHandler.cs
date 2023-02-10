using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetTeamMembersByUserIdQueryHandler : IRequestHandler<GetTeamMembersByUserIdQuery, IList<TeamMember>>
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetTeamMembersByUserIdQueryHandler(ITeamMemberRepository teamMemberRepository, IMapper mapper, IMediator mediator)
    {
        _teamMemberRepository = teamMemberRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IList<Domain.TeamMember>> Handle(GetTeamMembersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var teamMembers = _mapper.Map<IList<Domain.TeamMember>>(await _teamMemberRepository.GetQuery(x => x.UserId == request.Id));
        return teamMembers;
    }
}