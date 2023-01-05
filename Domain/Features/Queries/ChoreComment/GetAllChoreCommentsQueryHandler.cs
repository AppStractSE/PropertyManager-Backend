using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.ChoreComments;

public class GetAllChoreCommentsQueryHandler : IRequestHandler<GetAllChoreCommentsQuery, IList<ChoreComment>>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    public GetAllChoreCommentsQueryHandler(IChoreCommentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<ChoreComment>> Handle(GetAllChoreCommentsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<ChoreComment>>(await _repo.GetAllAsync());
    }
}