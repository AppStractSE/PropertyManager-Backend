using MediatR;

namespace Domain.Features.Commands.Area;

public class PutAreaCommand : IRequest<Domain.Area>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}