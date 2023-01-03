using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.Area;

public class PutAreaCommandHandler : IRequestHandler<PutAreaCommand, Domain.Area>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    public PutAreaCommandHandler(IAreaRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Area> Handle(PutAreaCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.PutAsync(_mapper.Map<Repository.Entities.Area>(request));
        return _mapper.Map<Domain.Area>(response);
    }
}