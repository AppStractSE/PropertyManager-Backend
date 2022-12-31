using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Chores;

public class GetChoreByIdQueryHandler : IRequestHandler<GetChoreByIdQuery, Chore>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    public GetChoreByIdQueryHandler(IChoreRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Chore> Handle(GetChoreByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<Chore>(await _repo.GetById(request.Id));
    }
}