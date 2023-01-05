using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Teams;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Team>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    public GetTeamByIdQueryHandler(ITeamRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Team> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<Team>(await _repo.GetById(request.Id));
    }
}