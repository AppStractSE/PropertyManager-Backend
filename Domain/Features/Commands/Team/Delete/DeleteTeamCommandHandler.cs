using Domain.Features.Commands.Team;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.User;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Domain.Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public DeleteTeamCommandHandler(ITeamRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }

     public async Task<Domain.Team> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.Team>(request));
         await _redisCache.RemoveAsync("Teams:");
        await _redisCache.RemoveAsync($"Team:{request.Id}");
        return _mapper.Map<Domain.Team>(response);
    }
}