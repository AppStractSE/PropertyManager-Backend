using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Team;

public class AddTeamCommandHandler : IRequestHandler<AddTeamCommand, Domain.Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddTeamCommandHandler(ITeamRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Team> Handle(AddTeamCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.Team>(request));
        await _cache.RemoveAsync("Teams:");
        return _mapper.Map<Domain.Team>(response);
    }
}