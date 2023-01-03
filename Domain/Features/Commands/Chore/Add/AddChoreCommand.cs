using MediatR;

namespace Domain.Features.Commands.Chore;

public class AddChoreCommand : IRequest<Domain.Chore>
{
    public string CategoryId { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
}