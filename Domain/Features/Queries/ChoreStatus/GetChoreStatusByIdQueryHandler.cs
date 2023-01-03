using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.ChoreStatuses;

public class GetChoreStatusByIdQueryHandler : IRequestHandler<GetChoreStatusByIdQuery, ChoreStatus>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    public GetChoreStatusByIdQueryHandler(IChoreStatusRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<ChoreStatus> Handle(GetChoreStatusByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<ChoreStatus>(await _repo.GetById(request.Id));
    }
}