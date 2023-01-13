using MediatR;

namespace Domain.Features.Queries.ChoreComments;

public class GetChoreCommentsByCustomerChoreIdQuery : IRequest<IList<Domain.ChoreComment>>
{
    public string Id { get; set; }
}