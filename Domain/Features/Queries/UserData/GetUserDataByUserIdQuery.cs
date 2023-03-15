using MediatR;

namespace Core.Features.Queries.UserData;

public class GetUserDataByUserIdQuery : IRequest<Domain.UserData>
{
    public string Id { get; set; }
}