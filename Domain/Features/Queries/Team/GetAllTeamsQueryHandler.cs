using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Teams;

public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, IList<Team>>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    public GetAllTeamsQueryHandler(ITeamRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Team>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<Team>>(await _repo.GetAllAsync());
    }
}