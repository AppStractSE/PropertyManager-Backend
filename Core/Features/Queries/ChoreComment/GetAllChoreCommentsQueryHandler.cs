using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.ChoreComments;

public class GetAllChoreCommentsQueryHandler : IRequestHandler<GetAllChoreCommentsQuery, IList<ChoreComment>>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAllChoreCommentsQueryHandler(IChoreCommentRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<IList<ChoreComment>> Handle(GetAllChoreCommentsQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("ChoreComments:"))
        {
            return await _cache.GetAsync<IList<ChoreComment>>("ChoreComments:");
        }

        var mappedCoreComment = _mapper.Map<IList<ChoreComment>>(await _repo.GetAllAsync());
        await _cache.SetAsync("ChoreComments:", mappedCoreComment);
        return mappedCoreComment;
    }
}