using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class DeleteCustomerChoreCommandHandler : IRequestHandler<DeleteCustomerChoreCommand, Domain.CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    private readonly IChoreCommentRepository _ccRepo;
    private readonly IChoreStatusRepository _csRepo;

    public DeleteCustomerChoreCommandHandler(ICustomerChoreRepository repo, IChoreCommentRepository ccRepo, IChoreStatusRepository csRepo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
        _csRepo = csRepo;
        _ccRepo = ccRepo;
    }

    public async Task<Domain.CustomerChore> Handle(DeleteCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.CustomerChore>(request));

        var comments = await _ccRepo.GetAllAsync();
        await _ccRepo.DeleteRangeAsync(comments.Where(x => x.CustomerChoreId == request.Id.ToString()));

        var statuses = await _csRepo.GetAllAsync();
        await _csRepo.DeleteRangeAsync(statuses.Where(x => x.CustomerChoreId == request.Id.ToString()));


        await _cache.RemoveAsync("CustomerChores:");
        await _cache.RemoveAsync($"CustomerChore:{request.Id}");
        return _mapper.Map<Domain.CustomerChore>(response);
    }
}