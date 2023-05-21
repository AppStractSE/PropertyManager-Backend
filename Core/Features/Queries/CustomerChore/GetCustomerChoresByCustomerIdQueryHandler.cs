using Core.Features.Queries.Chores;
using Core.Features.Queries.Customers;
using Core.Features.Queries.Periodics;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Core.Features.Queries.ChoreStatuses;
using Core.Features.Queries.Categories;

namespace Core.Features.Queries.CustomerChores;

public class GetCustomerChoresByCustomerIdQueryHandler : IRequestHandler<GetCustomerChoresByCustomerIdQuery, IList<Domain.CustomerChore>>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetCustomerChoresByCustomerIdQueryHandler(ICustomerChoreRepository repo, IMapper mapper, IMediator mediator)
    {
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IList<Domain.CustomerChore>> Handle(GetCustomerChoresByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var customerChores = _mapper.Map<IList<Domain.CustomerChore>>(await _repo.GetQuery(x => x.CustomerId == request.Id));
        var categories = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
        var chores = await _mediator.Send(new GetAllChoresQuery(), cancellationToken);
        var customers = await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
        var periodic = await _mediator.Send(new GetAllPeriodicsQuery(), cancellationToken);

        foreach (var customerChore in customerChores)
        {
            var allChoreStatuses = await _mediator.Send(new GetAllChoreStatusesQuery());
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
                customerChore.Progress = customerChoreProgress;
                customerChore.Status = customerChoreProgress == 0 ? "Ej påbörjad" : customerChoreProgress == customerChore.Frequency ? "Klar" : "Påbörjad";
                customerChore.Chore = chores.FirstOrDefault(x => x.Id.ToString() == customerChore.ChoreId);
                customerChore.Customer = customers.FirstOrDefault(x => x.Id.ToString() == customerChore.CustomerId);
                customerChore.SubCategoryName = categories.Where(category => category.SubCategories.Any(subcategory => subcategory.Id.ToString() == customerChore.Chore.SubCategoryId)).Select(category => category.SubCategories.First(subcategory => subcategory.Id.ToString() == customerChore.Chore.SubCategoryId).Title).FirstOrDefault();
                customerChore.Periodic = customerChorePeriodic;
                customerChore.DaysUntilReset = daysUntilReset;
            }
        }
        return customerChores;
    }
}