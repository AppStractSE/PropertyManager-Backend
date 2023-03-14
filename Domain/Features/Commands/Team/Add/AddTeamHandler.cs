using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Team;

public class AddTeamCommandHandler : IRequestHandler<AddTeamCommand, Domain.Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public AddTeamCommandHandler(ITeamRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Team> Handle(AddTeamCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.Team>(request));
        await _redisCache.RemoveAsync("Teams:");
        return _mapper.Map<Domain.Team>(response);
    }
}