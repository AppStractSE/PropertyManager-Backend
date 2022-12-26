using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Chores;

public class GetAllChoresQueryHandler : IRequestHandler<GetAllChoresQuery, IList<Chore>>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    public GetAllChoresQueryHandler(IChoreRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Chore>> Handle(GetAllChoresQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<Chore>>(await _repo.GetAllAsync());
    }
}