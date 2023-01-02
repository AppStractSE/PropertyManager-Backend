using MediatR;

namespace Domain.Features.Commands.Area;

public class AddAreaCommand : IRequest<Domain.Area>
{
    public string Name { get; set; }
}