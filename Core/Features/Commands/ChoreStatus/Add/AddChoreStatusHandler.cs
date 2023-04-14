using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Core.Features.Queries.Periodics;

namespace Core.Features.Commands.ChoreStatus;

public class AddChoreStatusCommandHandler : IRequestHandler<AddChoreStatusCommand, Domain.ChoreStatus>
{
    private readonly IChoreStatusRepository _repo;
    private readonly ICustomerChoreRepository _customerChoreRepo;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddChoreStatusCommandHandler(IChoreStatusRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.ChoreStatus> Handle(AddChoreStatusCommand request, CancellationToken cancellationToken)
    {
        //TODO: FORTSÄTT MED DENNA!
        // var periodics = await _mediator.Send(new GetAllPeriodicsQuery());
        // // var customerChore2 = await _mediator.Send(new GetCustomerChoreByCustomerChoreIdQuery(request.CustomerChoreId));
        // var customerChore = _customerChoreRepo
        //     .GetQuery(x => x.Id.ToString() == request.CustomerChoreId)
        //     .GetAwaiter()
        //     .GetResult()
        //     .FirstOrDefault();

        if (request.CompletedDate == null || request.CompletedDate == DateTime.MinValue)
        {
            request.CompletedDate = DateTime.Now;
        }
        else
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            request.CompletedDate = request.CompletedDate.AddHours(offset.Hours).AddMinutes(offset.Minutes);
        }

       return await AddChoreStatus(request);
    }

    private async Task<Domain.ChoreStatus> AddChoreStatus(AddChoreStatusCommand request)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.ChoreStatus>(request));
        await _cache.RemoveAsync($"ChoreStatuses:CustomerChore:{request.CustomerChoreId}");
        await _cache.RemoveAsync($"ChoreStatuses:CustomerChores");
        await _cache.RemoveAsync($"CustomerChores:");
        return _mapper.Map<Domain.ChoreStatus>(response);
    }
}