using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Areas;

public class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, Area>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    public GetAreaByIdQueryHandler(IAreaRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Area> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<Area>(await _repo.GetById(request.Id));
    }
}