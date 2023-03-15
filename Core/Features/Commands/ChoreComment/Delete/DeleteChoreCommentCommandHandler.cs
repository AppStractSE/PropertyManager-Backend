using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.ChoreComment;

public class DeleteChoreCommentCommandHandler : IRequestHandler<DeleteChoreCommentCommand, Domain.ChoreComment>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public DeleteChoreCommentCommandHandler(IChoreCommentRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Domain.ChoreComment> Handle(DeleteChoreCommentCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.ChoreComment>(request));
        await _cache.RemoveAsync("ChoreComments:");
        await _cache.RemoveAsync($"ChoreComment:{request.Id}");
        return _mapper.Map<Domain.ChoreComment>(response);
    }
}