using Core.Domain;
using Core.Features.Queries.CustomerChores;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Features.Queries.ChoreComments;

public class GetChoreCommentsByCustomerChoreIdQueryHandler : IRequestHandler<GetChoreCommentsByCustomerChoreIdQuery, IList<Domain.ChoreComment>>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRedisCache _redisCache;

    public GetChoreCommentsByCustomerChoreIdQueryHandler(IChoreCommentRepository repo, IMapper mapper, IMediator mediator, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<IList<Domain.ChoreComment>> Handle(GetChoreCommentsByCustomerChoreIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists($"Customer:ChoreComments:{request.Id}"))
        {
            return await _redisCache.GetAsync<IList<Domain.ChoreComment>>($"Customer:ChoreComments:{request.Id}");      
        }
        
        var ChoreComments = _mapper.Map<IList<Domain.ChoreComment>>(await _repo.GetQuery(x => x.CustomerChoreId == request.Id));
        var customerChores = await _mediator.Send(new GetAllCustomerChoresQuery(), cancellationToken);

        foreach (var ChoreComment in ChoreComments)
        {
            ChoreComment.CustomerChoreId = customerChores.FirstOrDefault(x => x.Id.ToString() == ChoreComment.CustomerChoreId).Id.ToString();
        }
        await _redisCache.SetAsync($"Customer:ChoreComments:{request.Id}", ChoreComments);
        return ChoreComments;
    }
}