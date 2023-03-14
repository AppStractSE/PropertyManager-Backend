using Core.Domain.Authentication;
using MediatR;

namespace Core.Features.Queries.Authentication.GetAllUsers;

public class GetAllUsersQuery : IRequest<IList<User>>
{
}