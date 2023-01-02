using MediatR;

namespace Domain.Features.Queries.Users;

public class GetAllUsersQuery : IRequest<IList<Domain.User>>
{

}