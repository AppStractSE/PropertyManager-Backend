using Core.Domain;
using Core.Features.Queries.Categories;
using Core.Features.Queries.Chores;
using Core.Features.Queries.ChoreStatuses;
using Core.Features.Queries.Customers;
using Core.Features.Queries.Periodics;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.CustomerChores;

public class GetCustomerChoreByIdQueryHandler : IRequestHandler<GetCustomerChoreByIdQuery, CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    private readonly IMediator _mediator;

    public GetCustomerChoreByIdQueryHandler(ICustomerChoreRepository repo, IMediator mediator, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<CustomerChore> Handle(GetCustomerChoreByIdQuery request, CancellationToken cancellationToken)
    {

        var customerChore = _mapper.Map<CustomerChore>(await _repo.GetById(request.Id));
        var chores = await _mediator.Send(new GetAllChoresQuery(), cancellationToken);
        var periodic = await _mediator.Send(new GetAllPeriodicsQuery(), cancellationToken);
        var customer = await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
        var allChoreStatuses = await _mediator.Send(new GetAllChoreStatusesQuery());
        var categories = await _mediator.Send(new GetAllCategoriesQuery());
        var customerChoreProgress = allChoreStatuses.Count(x => x.CustomerChoreId == customerChore.Id.ToString());
        var customerChorePeriodic = periodic.FirstOrDefault(y => y.Id.ToString() == customerChore.PeriodicId);
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
                    daysUntilReset = 1; // default value
                    break;
            }
        }
        customerChore.Progress = customerChoreProgress;
        customerChore.Status = customerChoreProgress == 0 ? "Ej påbörjad" : customerChoreProgress == customerChore.Frequency ? "Klar" : "Påbörjad";
        customerChore.Periodic = customerChorePeriodic;
        customerChore.DaysUntilReset = daysUntilReset;
        customerChore.SubCategoryName = categories.Where(category => category.SubCategories.Any(subcategory => subcategory.Id.ToString() == customerChore.Chore.SubCategoryId)).Select(category => category.SubCategories.First(subcategory => subcategory.Id.ToString() == customerChore.Chore.SubCategoryId).Title).FirstOrDefault();
        customerChore.Chore = chores.FirstOrDefault(x => x.Id.ToString() == customerChore.ChoreId);
        customerChore.Customer = customer.FirstOrDefault(x => x.Id.ToString() == customerChore.CustomerId);
        return customerChore;
    }
}