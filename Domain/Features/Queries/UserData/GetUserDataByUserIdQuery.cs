using MediatR;

namespace Domain.Features.Queries.UserData;

public class GetUserDataByUserIdQuery : IRequest<IList<Domain.UserData>>
{
    public string Id { get; set; }
}