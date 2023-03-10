using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.ChoreComment;

public class DeleteChoreCommentCommandHandler : IRequestHandler<DeleteChoreCommentCommand, Domain.ChoreComment>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public DeleteChoreCommentCommandHandler(IChoreCommentRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }

     public async Task<Domain.ChoreComment> Handle(DeleteChoreCommentCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.ChoreComment>(request));
         await _redisCache.RemoveAsync("ChoreComments:");
        await _redisCache.RemoveAsync($"ChoreComment:{request.Id}");
        return _mapper.Map<Domain.ChoreComment>(response);
    }
}