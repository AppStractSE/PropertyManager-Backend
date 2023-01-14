using Domain.Domain;
using Domain.Features.Queries.CustomerChores;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.ChoreStatuses;

public class GetChoreStatusByIdQueryHandler : IRequestHandler<GetChoreStatusByIdQuery, IList<Domain.ChoreStatus>>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    private IMediator _mediator;
    public GetChoreStatusByIdQueryHandler(IChoreStatusRepository repo, IMapper mapper, IMediator mediator)
    {
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<IList<Domain.ChoreStatus>> Handle(GetChoreStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var ChoreStatuses = _mapper.Map<IList<Domain.ChoreStatus>>(await _repo.GetQuery(x => x.CustomerChoreId == request.Id));
        var customerChores = await _mediator.Send(new GetAllCustomerChoresQuery());

        foreach (var ChoreStatus in ChoreStatuses)
        {
            ChoreStatus.CustomerChoreId = customerChores.FirstOrDefault(x => x.Id.ToString() == ChoreStatus.CustomerChoreId).Id.ToString();
        }

        return ChoreStatuses;
    }
}