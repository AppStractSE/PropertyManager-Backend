using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.ChoreStatuses;

public class GetAllChoreStatussQueryHandler : IRequestHandler<GetAllChoreStatusesQuery, IList<ChoreStatus>>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    public GetAllChoreStatussQueryHandler(IChoreStatusRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<ChoreStatus>> Handle(GetAllChoreStatusesQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<ChoreStatus>>(await _repo.GetAllAsync());
    }
}