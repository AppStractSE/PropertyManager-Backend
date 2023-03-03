using Core.Repository.Entities;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Features.Commands.ChoreComment;

public class AddChoreCommentCommandHandler : IRequestHandler<AddChoreCommentCommand, Domain.ChoreComment>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    private readonly UserManager<AuthUserEntity> _userManager;
    private readonly IRedisCache _redisCache;

    public AddChoreCommentCommandHandler(IChoreCommentRepository repo, IMapper mapper, UserManager<AuthUserEntity> userManager, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
        _userManager = userManager;
    }
    public async Task<Domain.ChoreComment> Handle(AddChoreCommentCommand request, CancellationToken cancellationToken)
    { 
        var choreComment = _mapper.Map<AddChoreCommentCommand, Domain.ChoreComment>(request);
        var user = await _userManager.FindByIdAsync(request.UserId);
        choreComment.DisplayName = user.DisplayName;
        choreComment.Time = DateTime.Now;
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.ChoreComment>(choreComment));
        await _redisCache.RemoveAsync($"ChoreComments:");
        return _mapper.Map<Domain.ChoreComment>(response);
    }
}