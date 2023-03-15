using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.ChoreComments;

public class GetLatestFiveChoreCommentsQueryHandler : IRequestHandler<GetLatestFiveChoreCommentsQuery, IList<ChoreComment>>
{
    private readonly IChoreCommentRepository _repo;
    private readonly IMapper _mapper;
    public GetLatestFiveChoreCommentsQueryHandler(IChoreCommentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<ChoreComment>> Handle(GetLatestFiveChoreCommentsQuery request, CancellationToken cancellationToken)
    {
        var choreComments = await _repo.GetAllAsync();
        return _mapper.Map<IList<ChoreComment>>(choreComments.OrderByDescending(x => x.Time).Take(5).ToList());
    }
}