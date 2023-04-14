using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.ChoreComment;

public class DeleteChoreCommentCommandHandler : IRequestHandler<DeleteChoreCommentCommand, bool>
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

    public async Task<bool> Handle(DeleteChoreCommentCommand request, CancellationToken cancellationToken)
    {
        var choreCommentToDelete = _repo.GetQuery(x => x.Id == request.Id).Result.FirstOrDefault();
        var response = await _repo.DeleteAsync(choreCommentToDelete);
        await _cache.RemoveAsync("ChoreComments:");
        await _cache.RemoveAsync($"ChoreComment:{choreCommentToDelete.CustomerChoreId}");
        return response;
    }
}