using Domain.Features.Commands.Team;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.User;

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Domain.Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public UpdateTeamCommandHandler(ITeamRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Team>(request));
        await _cache.RemoveAsync("Teams:");
        await _cache.RemoveAsync($"Team:{request.Id}");
        return _mapper.Map<Domain.Team>(response);
    }
}