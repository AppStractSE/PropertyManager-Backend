using MediatR;

namespace Domain.Features.Queries.Users;

public class GetUserByIdQuery : IRequest<Domain.User>
{
    public string Id { get; set; }
}