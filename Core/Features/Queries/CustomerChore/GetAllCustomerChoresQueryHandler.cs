using Core.Domain;
using Core.Features.Queries.ChoreStatuses;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.CustomerChores;

public class GetAllCustomerChoresQueryHandler : IRequestHandler<GetAllCustomerChoresQuery, IList<CustomerChore>>
{
    private readonly ICustomerChoreRepository _customerChoreRepo;
    private readonly IChoreRepository _choreRepo;
    private readonly IPeriodicRepository _periodicRepo;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICache _cache;

    public GetAllCustomerChoresQueryHandler(ICustomerChoreRepository customerChoreRepo, IChoreRepository choreRepo,
        IPeriodicRepository periodicRepo, IMapper mapper, ICache cache, IMediator mediator)
    {
        _customerChoreRepo = customerChoreRepo;
        _choreRepo = choreRepo;
        _periodicRepo = periodicRepo;
        _mapper = mapper;
        _cache = cache;
        _mediator = mediator;
    }

    public async Task<IList<CustomerChore>> Handle(GetAllCustomerChoresQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("CustomerChores:"))
        {
            return await _cache.GetAsync<IList<CustomerChore>>("CustomerChores:");
        }

        var chores = _mapper.Map<List<Chore>>(await _choreRepo.GetAllAsync());
        var periodic = _mapper.Map<List<Periodic>>(await _periodicRepo.GetAllAsync());
        var customerChores = await _customerChoreRepo.GetAllAsync();
        
        var mappedCustomerChores = customerChores.Select((x) =>
        {
            var allChoreStatuses = _mediator.Send(new GetAllChoreStatusesQuery()).GetAwaiter().GetResult();
            var progress = allChoreStatuses.Count(y => y.CustomerChoreId == x.Id.ToString());
            var customerChorePeriodic = periodic.FirstOrDefault(y => y.Id.ToString() == x.PeriodicId);
            var today = DateTime.Today;
            var daysUntilReset = 1;
            if (customerChorePeriodic != null)
            {
                switch (customerChorePeriodic.Name)
                {
                    case "Veckovis":
                        daysUntilReset = 7 - ((int)today.DayOfWeek + 6) % 7;
                        break;
                    case "Månadsvis":
                        daysUntilReset = DateTime.DaysInMonth(today.Year, today.Month) - today.Day;
                        break;
                    case "Årligen":
                        var endOfYear = new DateTime(today.Year, 12, 31);
                        daysUntilReset = (endOfYear - today).Days;
                        break;
                    default:
                        daysUntilReset = 1;
                        break;
                }
            }

            return new CustomerChore()
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                Chore = chores.FirstOrDefault(y => y.Id.ToString() == x.ChoreId),
                Frequency = x.Frequency,
                Periodic = customerChorePeriodic,
                Progress = progress,
                DaysUntilReset = daysUntilReset,
                Status = progress == 0 ? "Ej påbörjad" : progress == x.Frequency ? "Klar" : "Påbörjad",
            };
        }).ToList();

        await _cache.SetAsync("CustomerChores:", mappedCustomerChores);

        return mappedCustomerChores;
    }
}