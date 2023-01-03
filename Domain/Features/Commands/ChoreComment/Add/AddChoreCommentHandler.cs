using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.ChoreComment;

public class AddChoreCommentCommandHandler : IRequestHandler<AddChoreCommentCommand, Domain.ChoreComment>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    public AddChoreCommentCommandHandler(IChoreCommentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.ChoreComment> Handle(AddChoreCommentCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.ChoreComment>(request));
        return _mapper.Map<Domain.ChoreComment>(response);
    }
}