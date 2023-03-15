using MediatR;

namespace Core.Features.Commands.Chore;

public class UpdateChoreCommand : IRequest<Domain.Chore>
{
    public Guid Id { get; set; }
    public string CategoryId { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
}