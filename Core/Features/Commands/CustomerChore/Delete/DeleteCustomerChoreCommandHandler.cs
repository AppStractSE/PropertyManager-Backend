using Core.Features.Commands.ChoreComment;
using Core.Features.Commands.ChoreStatus;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class DeleteCustomerChoreCommandHandler : IRequestHandler<DeleteCustomerChoreCommand, bool>
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

    public async Task<bool> Handle(DeleteCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var result = false;
        var customerChoreToDelete = await _repo.GetById(request.Id.ToString());
        if (customerChoreToDelete != null)
        {
            result = await _repo.DeleteAsync(customerChoreToDelete);
        }

        if (result)
        {
            await _mediator.Send(new BulkDeleteChoreCommentsCommand { CustomerChoreId = request.Id.ToString() }, cancellationToken);
            await _mediator.Send(new BulkDeleteChoreStatusCommand { CustomerChoreId = request.Id.ToString() }, cancellationToken);

            await _cache.RemoveAsync("CustomerChores:");
            await _cache.RemoveAsync($"CustomerChore:{request.Id}");
        }

        return result;
    }
}