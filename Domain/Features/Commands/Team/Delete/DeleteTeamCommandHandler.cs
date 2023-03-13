using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.Team;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Domain.Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public DeleteTeamCommandHandler(ITeamRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Domain.Team> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.Team>(request));
        await _cache.RemoveAsync("Teams:");
        await _cache.RemoveAsync($"Team:{request.Id}");
        return _mapper.Map<Domain.Team>(response);
    }
}