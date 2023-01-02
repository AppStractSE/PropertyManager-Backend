using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.CustomerChores;

public class GetAllCustomerChoresQueryHandler : IRequestHandler<GetAllCustomerChoresQuery, IList<CustomerChore>>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    public GetAllCustomerChoresQueryHandler(ICustomerChoreRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<CustomerChore>> Handle(GetAllCustomerChoresQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<CustomerChore>>(await _repo.GetAllAsync());
    }
}