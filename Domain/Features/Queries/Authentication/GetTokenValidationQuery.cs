using Core.Domain.Authentication;
using MediatR;

namespace Core.Features.Authentication.Queries
{
    public class GetTokenValidationQuery : IRequest<AuthUser>
    {
        public string Token { get; set; } = "";
    }
}
