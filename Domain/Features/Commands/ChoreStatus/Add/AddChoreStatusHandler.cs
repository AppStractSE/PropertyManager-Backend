using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.ChoreStatus;

public class AddChoreStatusCommandHandler : IRequestHandler<AddChoreStatusCommand, Domain.ChoreStatus>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    public AddChoreStatusCommandHandler(IChoreStatusRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.ChoreStatus> Handle(AddChoreStatusCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.ChoreStatus>(request));
        response.StartDate = DateTime.Now;
        response.CompletedDate = DateTime.Now;
        return _mapper.Map<Domain.ChoreStatus>(response);
    }
}