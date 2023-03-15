using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.ChoreComment;

public class BulkDeleteChoreCommentsCommandHandler : IRequestHandler<BulkDeleteChoreCommentsCommand, bool>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public BulkDeleteChoreCommentsCommandHandler(IChoreCommentRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<bool> Handle(BulkDeleteChoreCommentsCommand request, CancellationToken cancellationToken)
    {
        var comments = await _repo.GetAllAsync();
        var commentsToDelete = comments.Where(x => request.CustomerChoreId == x.CustomerChoreId.ToString());
        var result = await _repo.DeleteRangeAsync(commentsToDelete);

        if (result)
        {
            commentsToDelete.ToList().ForEach(async x =>
            {
                await _cache.RemoveAsync($"ChoreComment:{x.Id}");
            });
            await _cache.RemoveAsync("ChoreComments:");
        }

        return result;
    }
}