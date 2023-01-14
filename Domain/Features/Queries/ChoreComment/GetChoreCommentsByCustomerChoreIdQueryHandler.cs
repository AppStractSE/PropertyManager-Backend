using Domain.Domain;
using Domain.Features.Queries.CustomerChores;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Features.Queries.ChoreComments;

public class GetChoreCommentsByCustomerChoreIdQueryHandler : IRequestHandler<GetChoreCommentsByCustomerChoreIdQuery, IList<Domain.ChoreComment>>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    private ILogger<GetChoreCommentsByCustomerChoreIdQueryHandler> _logger;
    private IMediator _mediator;
    public GetChoreCommentsByCustomerChoreIdQueryHandler(IChoreCommentRepository repo, IMapper mapper, ILogger<GetChoreCommentsByCustomerChoreIdQueryHandler> logger, IMediator mediator)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }
    public async Task<IList<Domain.ChoreComment>> Handle(GetChoreCommentsByCustomerChoreIdQuery request, CancellationToken cancellationToken)
    {
        var ChoreComments = _mapper.Map<IList<Domain.ChoreComment>>(await _repo.GetQuery(x => x.CustomerChoreId == request.Id));
        var customerChores = await _mediator.Send(new GetAllCustomerChoresQuery());

        foreach (var ChoreComment in ChoreComments)
        {
            ChoreComment.CustomerChoreId = customerChores.FirstOrDefault(x => x.Id.ToString() == ChoreComment.CustomerChoreId).Id.ToString();
        }

        return ChoreComments;
    }
}