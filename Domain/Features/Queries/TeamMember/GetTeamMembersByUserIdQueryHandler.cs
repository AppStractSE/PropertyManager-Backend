using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.TeamMembers;

public class GetTeamMembersByUserIdQueryHandler : IRequestHandler<GetTeamMembersByUserIdQuery, IList<TeamMember>>
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetTeamMembersByUserIdQueryHandler(ITeamMemberRepository teamMemberRepository, IMapper mapper, IMediator mediator, ICache cache)
    {
        _cache = cache;
        _teamMemberRepository = teamMemberRepository;
        _mapper = mapper;
    }

    public async Task<IList<Domain.TeamMember>> Handle(GetTeamMembersByUserIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists($"User:TeamMembers:{request.Id}"))
        {
            return await _cache.GetAsync<IList<Domain.TeamMember>>($"User:TeamMembers:{request.Id}");
        }

        var teamMembers = _mapper.Map<IList<Domain.TeamMember>>(await _teamMemberRepository.GetQuery(x => x.UserId == request.Id));
        await _cache.SetAsync($"User:TeamMembers:{request.Id}", teamMembers);
        return teamMembers;
    }
}