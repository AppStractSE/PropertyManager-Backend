using Domain.Features.Commands.Team;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.User;

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Domain.Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public UpdateTeamCommandHandler(ITeamRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Team>(request));
        await _redisCache.RemoveAsync("Teams:");
        await _redisCache.RemoveAsync($"Team:{request.Id}");
        return _mapper.Map<Domain.Team>(response);
    }
}