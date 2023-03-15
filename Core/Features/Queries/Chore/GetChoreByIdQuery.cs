using MediatR;

namespace Core.Features.Queries.Chores;

public class GetChoreByIdQuery : IRequest<Domain.Chore>
{
    public string Id { get; set; }
}