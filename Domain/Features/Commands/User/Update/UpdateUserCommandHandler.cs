using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.Team;

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Domain.Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    public UpdateTeamCommandHandler(ITeamRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Team>(request));
        return _mapper.Map<Domain.Team>(response);
    }
}