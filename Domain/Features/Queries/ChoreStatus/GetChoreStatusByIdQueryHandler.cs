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
    private readonly IRedisCache _redisCache;

    public GetChoreStatusByIdQueryHandler(IChoreStatusRepository repo, IMapper mapper, IMediator mediator, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public async Task<IList<Domain.ChoreStatus>> Handle(GetChoreStatusByIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("ChoreStatuses:"))
        {
            return await _redisCache.GetAsync<IList<Domain.ChoreStatus>>("ChoreStatuses:");
        }
        
        var ChoreStatuses = _mapper.Map<IList<Domain.ChoreStatus>>(await _repo.GetQuery(x => x.CustomerChoreId == request.Id));
        var customerChores = await _mediator.Send(new GetAllCustomerChoresQuery(), cancellationToken);

        foreach (var ChoreStatus in ChoreStatuses)
        {
            ChoreStatus.CustomerChoreId = customerChores.FirstOrDefault(x => x.Id.ToString() == ChoreStatus.CustomerChoreId).Id.ToString();
        }

        await _redisCache.SetAsync($"ChoreStatus:{request.Id}", ChoreStatuses);
        return ChoreStatuses;
    }
}