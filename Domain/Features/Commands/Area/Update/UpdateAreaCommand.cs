using MediatR;

namespace Domain.Features.Commands.Area;

public class UpdateAreaCommand : IRequest<Domain.Area>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}