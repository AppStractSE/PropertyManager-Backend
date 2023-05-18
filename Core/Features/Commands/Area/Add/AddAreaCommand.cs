using MediatR;

namespace Core.Features.Commands.Area;

public class AddAreaCommand : IRequest<Domain.Area>
{
    public string Name { get; set; }
    public string CityId { get; set; }
}