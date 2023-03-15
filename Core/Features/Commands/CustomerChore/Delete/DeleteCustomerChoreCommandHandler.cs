using Core.Features.Commands.ChoreComment;
using Core.Features.Commands.ChoreStatus;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class DeleteCustomerChoreCommandHandler : IRequestHandler<DeleteCustomerChoreCommand, Domain.CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    private readonly IMediator _mediator;
    private readonly IChoreStatusRepository _csRepo;

    public DeleteCustomerChoreCommandHandler(ICustomerChoreRepository repo, IMediator mediator, IChoreStatusRepository csRepo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
        _csRepo = csRepo;
        _mediator = mediator;
    }

    public async Task<Domain.CustomerChore> Handle(DeleteCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.CustomerChore>(request));

        await _mediator.Send(new BulkDeleteChoreCommentsCommand { CustomerChoreId = request.CustomerChoreId.ToString() });
        await _mediator.Send(new BulkDeleteChoreStatusesCommand { CustomerChoreId = request.CustomerChoreId.ToString() });

        await _cache.RemoveAsync("CustomerChores:");
        await _cache.RemoveAsync($"CustomerChore:{request.CustomerChoreId}");
        return _mapper.Map<Domain.CustomerChore>(response);
    }
}