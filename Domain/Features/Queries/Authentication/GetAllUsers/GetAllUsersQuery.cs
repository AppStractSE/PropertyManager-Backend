using Domain.Domain.Authentication;
using MediatR;

namespace Domain.Features.Queries.Authentication.GetAllUsers;

public class GetAllUsersQuery : IRequest<IList<AuthUser>>
{
}