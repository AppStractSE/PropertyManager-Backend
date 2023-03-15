using MediatR;

namespace Core.Features.Commands.Area;

public class DeleteAreaCommand : IRequest<Domain.Area>
{
    public Guid Id { get; set; }
}