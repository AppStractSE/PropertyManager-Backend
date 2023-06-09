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
    private readonly ICache _cache;

    public GetChoreCommentsByCustomerChoreIdQueryHandler(IChoreCommentRepository repo, IMapper mapper, IMediator mediator, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<IList<Domain.ChoreComment>> Handle(GetChoreCommentsByCustomerChoreIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists($"ChoreComment:{request.Id}"))
        {
            return await _cache.GetAsync<IList<Domain.ChoreComment>>($"ChoreComment:{request.Id}");
        }

        var choreComments = _mapper.Map<IList<Domain.ChoreComment>>(await _repo.GetQuery(x => x.CustomerChoreId == request.Id));
        var customerChores = await _mediator.Send(new GetAllCustomerChoresQuery(), cancellationToken);

        foreach (var choreComment in choreComments)
        {
            choreComment.CustomerChoreId = customerChores.FirstOrDefault(x => x.Id.ToString() == choreComment.CustomerChoreId).Id.ToString();
        }
        await _cache.SetAsync($"ChoreComment:{request.Id}", choreComments);
        return choreComments;
    }
}