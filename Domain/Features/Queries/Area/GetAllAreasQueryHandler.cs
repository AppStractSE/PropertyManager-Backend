using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Areas;

public class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, IList<Area>>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    public GetAllAreasQueryHandler(IAreaRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Area>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<Area>>(await _repo.GetAllAsync());
    }
}