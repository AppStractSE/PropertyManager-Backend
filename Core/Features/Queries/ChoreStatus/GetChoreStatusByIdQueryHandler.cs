using Core.Domain;
using Core.Features.Queries.CustomerChores;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.ChoreStatuses;

public class GetChoreStatusByIdQueryHandler : IRequestHandler<GetChoreStatusByIdQuery, IList<Domain.ChoreStatus>>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICache _cache;

    public GetChoreStatusByIdQueryHandler(IChoreStatusRepository repo, IMapper mapper, IMediator mediator, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IList<Domain.ChoreStatus>> Handle(GetChoreStatusByIdQuery request, CancellationToken cancellationToken) //Is it used the way it should be? Returns ChoreStatuses by CustomerChoreId. Change name maybe?
    {
        if (_cache.Exists($"ChoreStatuses:CustomerChore:{request.Id}"))
        {
            return await _cache.GetAsync<IList<Domain.ChoreStatus>>($"ChoreStatuses:{request.Id}");
        }

        var choreStatuses = _mapper.Map<IList<Domain.ChoreStatus>>(await _repo.GetQuery(x => x.CustomerChoreId == request.Id));

        // var customerChores = await _mediator.Send(new GetAllCustomerChoresQuery(), cancellationToken); // And possibly this one too

        // foreach (var ChoreStatus in ChoreStatuses) // Possibly remove this
        // {
        //     ChoreStatus.CustomerChoreId = customerChores.FirstOrDefault(x => x.Id.ToString() == ChoreStatus.CustomerChoreId).Id.ToString();
        // }

        await _cache.SetAsync($"ChoreStatuses:CustomerChore:{request.Id}", choreStatuses);
        return choreStatuses;
    }
}