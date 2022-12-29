using Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Features.Queries.CustomerChore;

public class GetCustomerChoresByCustomerIdQueryHandler : IRequestHandler<GetCustomerChoresByCustomerIdQuery, IList<Domain.CustomerChore>>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private ILogger<GetCustomerChoresByCustomerIdQueryHandler> _logger;
    public GetCustomerChoresByCustomerIdQueryHandler(ICustomerChoreRepository repo, IMapper mapper, ILogger<GetCustomerChoresByCustomerIdQueryHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IList<Domain.CustomerChore>> Handle(GetCustomerChoresByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<Domain.CustomerChore>>(await _repo.GetQuery(x => x.CustomerId == request.Id));
    }
}