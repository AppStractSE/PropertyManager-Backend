using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Teams;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetTeamByIdQueryHandler(ITeamRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Team> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists($"Team:{request.Id}"))
        {
            return await _cache.GetAsync<Team>($"Team:{request.Id}");
        }

        var mappedDomain = _mapper.Map<Team>(await _repo.GetById(request.Id));
        await _cache.SetAsync($"Team:{request.Id}", mappedDomain);
        return mappedDomain;
    }
}