using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Teams;

public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, IList<Team>>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAllTeamsQueryHandler(ITeamRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Team>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("Teams:"))
        {
            return await _cache.GetAsync<IList<Team>>("Teams:");
        }

        var mappedDomain = _mapper.Map<IList<Team>>(await _repo.GetAllAsync());
        await _cache.SetAsync("Teams:", mappedDomain);
        return mappedDomain;
    }
}