using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.ChoreComments;

public class GetAllChoreCommentsQueryHandler : IRequestHandler<GetAllChoreCommentsQuery, IList<ChoreComment>>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetAllChoreCommentsQueryHandler(IChoreCommentRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
    }
    public async Task<IList<ChoreComment>> Handle(GetAllChoreCommentsQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("ChoreComments:"))
        {
            return await _redisCache.GetAsync<IList<ChoreComment>>("ChoreComments:");
        }

        var mappedCoreComment = _mapper.Map<IList<ChoreComment>>(await _repo.GetAllAsync());
        await _redisCache.SetAsync("ChoreComments:", mappedCoreComment);
        return mappedCoreComment;
    }
}