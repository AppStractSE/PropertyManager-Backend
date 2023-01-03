using MediatR;

namespace Domain.Features.Queries.Areas;

public class GetAreaByIdQuery : IRequest<Domain.Area>
{
    public string Id { get; set; }
}