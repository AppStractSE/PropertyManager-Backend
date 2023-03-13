using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.CustomerChores;

public class GetAllCustomerChoresQueryHandler : IRequestHandler<GetAllCustomerChoresQuery, IList<CustomerChore>>
{
    private readonly ICustomerChoreRepository _customerChoreRepo;
    private readonly IChoreRepository _choreRepo;
    private readonly IPeriodicRepository _periodicRepo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAllCustomerChoresQueryHandler(ICustomerChoreRepository customerChoreRepo, IChoreRepository choreRepo,
        IPeriodicRepository periodicRepo, IMapper mapper, ICache cache)
    {
        _customerChoreRepo = customerChoreRepo;
        _choreRepo = choreRepo;
        _periodicRepo = periodicRepo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<IList<CustomerChore>> Handle(GetAllCustomerChoresQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("CustomerChores:"))
        {
            return await _cache.GetAsync<IList<CustomerChore>>("CustomerChores:");
        }

        var cores = _mapper.Map<List<Chore>>(await _choreRepo.GetAllAsync());
        var periodic = _mapper.Map<List<Periodic>>(await _periodicRepo.GetAllAsync());
        var customerChores = await _customerChoreRepo.GetAllAsync();

        var mappedCustomerChores = customerChores.Select(x => new CustomerChore()
        {
            Id = x.Id,
            CustomerId = x.CustomerId,
            Chore = cores.FirstOrDefault(y => y.Id.ToString() == x.ChoreId),
            Frequency = x.Frequency,
            Periodic = periodic.FirstOrDefault(y => y.Id.ToString() == x.PeriodicId),
        }).ToList();

        await _cache.SetAsync("CustomerChores:", mappedCustomerChores);
        return mappedCustomerChores;
    }
}