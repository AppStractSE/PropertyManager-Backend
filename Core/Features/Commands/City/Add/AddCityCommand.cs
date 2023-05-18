using MediatR;

namespace Core.Features.Commands.City;

public class AddCityCommand : IRequest<Domain.City>
{
    public string Name { get; set; }
}